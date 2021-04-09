<template>
  <v-container>
    <v-card outlined tile :loading="this.loading" v-if="this.stock.displayName">
      <v-card-title>Monthly price table: </v-card-title>

      <v-layout justify-center>
        <v-data-table
          fluid
          :headers="headers"
          :items="tableData"
          :items-per-page="5"
          class="elevation-1"
        >
          <template v-slot:item.trend="{ item }">
            <v-icon :color="item.color" x-large>{{ item.trend }}</v-icon>
          </template>
          <template v-slot:item.timestamp="{ item }">
            <span>{{ new Date(item.timestamp).toDateString() }}</span>
          </template>
          <template v-slot:item.closingPrice="{ item }">
            <span :style="{ color: item.color }"> {{ item.closingPrice }}</span>
          </template>
        </v-data-table>
      </v-layout>
    </v-card>
  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "StockPriceTable",
  components: {},
  data() {
    return {
      tableData: [],
      headers: [
        { text: "Timestamp", value: "timestamp" },
        { text: "High", value: "high" },
        { text: "Low", value: "low" },
        { text: "Value", value: "closingPrice" },
        { text: "Trend", value: "trend", sortable: false },
      ],
    };
  },
  watch: {
    history(data) {
      this.tableData = data.history.map((currentValue, index, array) => {
        let icon = "";
        let color = "green";
        var previousDay = array[index + 1];
        if (previousDay) {
          if (currentValue.closingPrice > previousDay.closingPrice) {
            icon = "mdi-trending-up";
            color = "green";
          } else {
            icon = "mdi-trending-down";
            color = "red";
          }
        }

        return { ...currentValue, trend: icon, color: color };
      });
    },
  },
  computed: { ...mapState(["stock", "history", "loading"]) },
  mounted() {},
  methods: {},
};
</script>