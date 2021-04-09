<template>
  <v-container>
    <v-card outlined tile  :loading="this.loading" v-if="this.stock.displayName">
      <v-card-title
        >Stock Recommendation:       </v-card-title>
      <v-card-text>
        <v-row align="center"> '{{ this.stock.displayName }} ({{
          this.stock.ticker
        }})':
        <v-spacer/>
        <v-chip>
          {{ this.recommendation.recommendation }}
        </v-chip>
</v-row>
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
    return {};
  },
  computed: { ...mapState(["stock", "recommendation","loading"]) },
  mounted() {},
  methods: {},
};
</script>