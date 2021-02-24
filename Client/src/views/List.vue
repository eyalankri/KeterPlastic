<template>
  <div class="home">
    <v-tabs>
      <v-tab>
        <v-icon left>edit</v-icon>Edit Products
      </v-tab>
      <v-tab>
        <v-icon left>store</v-icon>Another Tab
      </v-tab>

      <v-tab-item>
        <v-card flat>
          <v-container>
            <v-row>
              <Products   />
            </v-row>
          </v-container>
        </v-card>
      </v-tab-item>

        <v-tab-item>
        <v-card flat>
          <v-container>             
            <div>Another tab in this page...</div>
          </v-container>
        </v-card>
      </v-tab-item>
     
    </v-tabs>
  </div>
</template>


<script>
import Products from "@/components/Products";
export default {
  components: { Products },
  data: () => ({
   
  }),
  mounted(){
   
  },
  methods: {
   
  },
   
};
</script>