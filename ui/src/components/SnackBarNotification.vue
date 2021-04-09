<template>
  <v-container>
    <v-snackbar
      :multi-line="true"
      :right="true"
      :timeout="3500"
      :top="true"
      :vertical="true"
      :color="snackbar.color"
      v-model="snackbar.show"
    >
    <template v-slot:action="{ attrs }">
        <v-btn
          text
          v-bind="attrs"
          @click="snackbar.show = false"
        >
          Close
        </v-btn>
      </template>

      <h3>{{ snackbar.event.title }}</h3>
      <v-divider class="mx-2"></v-divider>
      {{ snackbar.event.data.detail }}
      <v-divider class="mx-2"></v-divider>
    </v-snackbar>
  </v-container>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "SnackBarNotification",
  components: {},
  computed: {
    ...mapState(["error"]),
  },
  data() {
    return {
      snackbar: {
        show: false,
        color: null,
        event: { title: "", data: {} },
      },
    };
  },
  watch: {
    error(event) {
      if (event.response) {
        this.snackbar = {
          color: "error",
          show: true,
          event: { title: event, data: event.response.data },
        };
      } else {
        console.error(event);
      }
    },
  },
};
</script>
