<template>
  <v-container>
    <v-card outlined tile :loading="this.loading" v-if="this.stock.displayName">
      <v-card-title
        >Stock Information: <v-spacer />'{{ this.stock.displayName }} ({{
          this.stock.ticker
        }})':
      </v-card-title>
      <v-card-text>
        <v-row align="center"> High:{{ this.stock.dailyHigh }} </v-row>
        <v-row align="center"> Low:{{ this.stock.dailyLow }} </v-row>
        <v-row align="center"> Price:{{ this.stock.dailyLow }} </v-row>
      </v-card-text>
      <v-divider></v-divider>

      <v-card-text>
        <v-row align="center">
          Recommendation: <v-spacer />
          <v-chip>{{ this.recommendation.recommendation }} </v-chip>
        </v-row>
        <v-row align="center"> Reason: {{ this.recommendation.reason }} </v-row>
      </v-card-text>

      <v-divider></v-divider>
      <v-card-text>
        <v-row align="center">
          Yearly trend: {{ sparklineData[0].year }} -
          {{ sparklineData[sparklineData.length - 1].year }}
        </v-row>
        <v-sheet color="rgba(0, 0, 0, .12)">
          <v-sparkline :value="sparklineData.map((v)=>{return v.value})" stroke-linecap="round" smooth>
            <template v-slot:label="item"> ${{ item.value }} </template>
          </v-sparkline>
        </v-sheet>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "StockRecommendation",
  components: {},
  data() {
    return {
      sparklineData: [],
    };
  },
  watch: {
    history(data) {
      this.sparklineData = data.history
        .filter(function (currentValue) {
          //Only pick january months
          var parsedDte = new Date(currentValue.timestamp);
          if (parsedDte.getMonth() == 0) return true;
          return false;
        })
        .map((currentValue) => {
          return {
            value: currentValue.closingPrice,
            year: new Date(currentValue.timestamp).getFullYear(),
          };
        })
        .slice(0, 5).reverse();
    },
  },
  computed: { ...mapState(["stock", "recommendation", "history", "loading"]) },
  mounted() {},
  methods: {},
};
</script>