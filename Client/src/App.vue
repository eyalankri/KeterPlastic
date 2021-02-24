<template>
  <v-app>
    <Navbar />

    <v-content>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar";
import axios from "axios";
export default {
  name: "App",
  components: { Navbar },
  data: () => ({
    //
  }),
  mounted() {
     
  },
  /* eslint-disable */

  
};
</script>
