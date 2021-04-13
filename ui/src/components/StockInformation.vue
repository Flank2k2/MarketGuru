<template>
  <v-container>
    <v-card outlined tile :loading="this.loading" v-if="this.stock.displayName">
      <v-card-title
        >Stock Information: <v-spacer /> {{ this.stock.displayName }} ({{
          this.stock.ticker
        }})
      </v-card-title>
      <v-card-text>
        <v-row no-gutters>
          <v-col align="center" cols="12" sm="4">
           <strong> Closing Price:</strong><v-spacer />{{ this.stock.closingPrice }}
          </v-col>
          <v-col align="center" cols="12" sm="4">
            <strong>Daily high: </strong><v-spacer />{{ this.stock.dailyHigh }}
          </v-col>
          <v-col align="center" cols="12" sm="4">
          <strong>  Daily low:</strong><v-spacer />{{ this.stock.dailyLow }}
          </v-col>
        </v-row>
      </v-card-text>
      <v-divider></v-divider>

      <v-card-text>
        <v-list two-line>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Recommendation</v-list-item-title>
              <v-list-item-subtitle>{{
                this.recommendation.reason
              }}</v-list-item-subtitle>
            </v-list-item-content>

            <v-list-item-icon>
              <v-chip>{{ this.recommendation.recommendation }} </v-chip>
            </v-list-item-icon>
          </v-list-item>
          
      <v-divider></v-divider>

          <v-list-item>
            <v-list-item-content>
              <v-list-item-title>Yearly trend:</v-list-item-title>
              <v-list-item-subtitle
                >{{ sparklineData[0].year }} -
                {{
                  sparklineData[sparklineData.length - 1].year
                }}</v-list-item-subtitle
              >

              <v-sheet color="rgba(0, 0, 0, .12)" class="pa-3 ma-6">
                <v-sparkline
                  v-if="this.sparklineData"
                  :value="
                    sparklineData.map((v) => {
                      return v.value;
                    })
                  "
                  stroke-linecap="round"
                  height="100"
                  padding="24"
                  smooth
                >
                  <template v-slot:label="item"> ${{ item.value }} </template>
                </v-sparkline>
              </v-sheet>
            </v-list-item-content>
          </v-list-item>
        </v-list>
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
      if (!data.history) {
        this.sparklineData = [];
        return;
      }

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
        .slice(0, 5)
        .reverse();
    },
  },
  computed: { ...mapState(["stock", "recommendation", "history", "loading"]) },
  mounted() {},
  methods: {},
};
</script>