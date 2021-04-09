<template>
  <v-container>
    <v-card outlined tile :loading="this.loading" v-if="this.stock.displayName">
      <v-card-title>Price Table: </v-card-title>
      <v-card-text>
        <v-row> </v-row>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "StockPriceTable",
  components: {},
  data() {
    return {};
  },
  computed: { ...mapState(["stock", "recommendation", "loading"]) },
  mounted() {},
  methods: {},
};
</script>