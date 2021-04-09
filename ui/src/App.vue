<template>
  <v-app>
    <v-app-bar app color="primary">
      <v-toolbar-title dark class="white--text"
        >Welcome to Market Guru !
      </v-toolbar-title>
      <v-spacer></v-spacer>
    </v-app-bar>

    <v-main>
      <ErrorNotifications />

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
        <StockInformation />
        <StockPriceTable />
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

import StockPriceTable from "./components/StockPriceTable";
import StockInformation from "./components/StockInformation";
import ErrorNotifications from "./components/ErrorNotifications";

export default {
  name: "App",

  components: {
    ErrorNotifications,
    StockPriceTable,
    StockInformation,
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