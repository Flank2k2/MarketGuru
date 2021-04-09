<template>
  <v-app>
    <v-app-bar app color="primary">
      <v-toolbar-title dark class="white--text"
        >Welcome to Market Guru !
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <SnackBarNotification
        v-on:error="onError($event)"
        v-on:success="onSuccess($event)"
      />

      <v-container fluid>
        <v-container>
          <v-form
            v-model="valid"
            lazy-validation
            ref="form"
            v-on:submit.prevent
          >
            <v-row>
              <v-text-field v-model="ticker" :rules="ruleTest" required>
                <template v-slot:label> Enter a stock symbol: </template>
              </v-text-field>
            </v-row>
            <v-row align="center" justify="center">
              <v-btn :disabled="!valid" @click="getRecommendation" outlined
                >Get recommendation</v-btn
              >
            </v-row>
          </v-form>
        </v-container>
        <StockRecommendation />
        <StockChart />
      </v-container>
    </v-main>
    <v-footer color="primary">
      <v-col class="text-center" cols="12" white>
        {{ new Date().getFullYear() }} — Market Guru
      </v-col>
    </v-footer>
  </v-app>
</template>

<script>
import { mapActions } from "vuex";

import StockChart from "./components/StockChart";
//import StockPriceTable from "./components/StockPriceTable";
import StockRecommendation from "./components/StockRecommendation";
import SnackBarNotification from "./components/SnackBarNotification";

export default {
  name: "App",

  components: {
    SnackBarNotification,
    StockChart,
    //  StockPriceTable,
    StockRecommendation,
  },

  data: () => ({
    ticker: "",
    valid: false,
    ruleTest: [
      function (v) {
        if (v) return true;
        return "Ticker is missing";
      },
    ],
  }),
  errorCaptured: function (err) {
    this.$root.$emit("error", err);
    return false;
  },
  methods: {
    getRecommendation: async function () {
      var rst = this.$refs.form.validate();
      if (rst == false) return;
      console.log("Retrieving stock data", this.ticker, rst);
      this.getStockDataFromApi(this.ticker);
    },
    ...mapActions(["getStockDataFromApi"]),
  },
};
</script>