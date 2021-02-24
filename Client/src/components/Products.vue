<template>
  <div class="products">
    <v-container>
      <v-row class="mx-auto">
        <v-spacer></v-spacer>
        <v-btn fab dark color="red" @click="addProductShowModal">
          <v-icon dark>mdi-plus</v-icon>
        </v-btn>
      </v-row>
      <v-row class="mx-auto">
        Sort by:
        <v-btn class="mx-2" depressed small @click="sort('price')">Price</v-btn>
        <v-btn class="mx-2" depressed small @click="sort('name')">Name</v-btn>
        <v-btn class="mx-2" depressed small @click="sort('date')">Date</v-btn>
      </v-row>
     
      <v-row>
        <div v-for="prod in products" :key="prod.id" style="display:contents">
          <v-col v-if="!prod.selected" cols="12" md="6" lg="3" style="width:344px">
            <v-card class="mx-auto" justify-center style="width:344px">
              <v-card-title>{{prod.productName}}</v-card-title>
              <v-img :src="(`../Uploads/${prod.productFileName}`)" max-height="200" contain />

              <v-card-subtitle class="overline">
                <span class="red--text">
                  price:
                  <span>${{prod.productPriceUsd}}</span>
                </span>
                <br />
                Date Created: {{prod.dateCreated}}
                <br />
                Last Updated: {{prod.dateCreated}}
              </v-card-subtitle>
              <v-card-actions>
                <v-btn text @click="removeProduct(prod.productId)">remove</v-btn>
                <v-btn text @click="editProductShowModal(prod.productId)">edit</v-btn>
                <v-spacer></v-spacer>
              </v-card-actions>
              <v-expand-transition>
                <div>
                  <v-divider></v-divider>
                  <v-card-text>{{prod.productDescription}}</v-card-text>
                </div>
              </v-expand-transition>
            </v-card>
          </v-col>
        </div>
      </v-row>
    </v-container>
    <modal name="addOrEditProduct" :height="500" :width="380" >
      <v-row style="padding:40px">
        <v-form ref="form" v-model="validForm" :lazy-validation="lazy">
          <v-text-field
            v-model="productName"
            :counter="30"
            :rules="productNameRules"
            label="Product Name"
          ></v-text-field>
          <v-text-field
            v-model="productDescription"
            :counter="100"
            :rules="productDescriptionRules"
            label="Product Description"
            required
          ></v-text-field>
          <v-text-field
            type="number"
            v-model="productPriceUsd"
            :rules="productPriceUsdRules"
            label="Product Price"
            required
          ></v-text-field>

          <v-file-input
            v-if="uploadFile"
            label="File input"
            v-model="productPhoto"
            :rules="productPhotoRules"
            prepend-icon="mdi-camera"
            required
            ref="myFileInput"
          ></v-file-input>

          <v-checkbox
            v-if="this.productId"
            @change="changeUploadFile"
            label="change photo"
            color="orange"
            hide-details
            ref="fileInput"
          ></v-checkbox>

          <br />

          <v-btn
            color="warning"
            v-if="!this.productId"
            :disabled="!validForm"
            @click="insertProduct"
          >Insert Product</v-btn>
          <v-btn
            color="warning"
            v-if="this.productId"
            :disabled="!validForm"
            @click="updateProduct"
          >Update Product</v-btn>
        </v-form>
      </v-row>
    </modal>
  </div>
</template>


<script>
import moment from "moment";
import axios from "axios";

export default {
  data: () => ({
    uploadFile: true,
    validForm: true,
    products: null,

    productId: null,
    productName: "",
    productFileName: "",

    productNameRules: [
      v => !!v || "Product Name is required",
      v => (v && v.length < 30) || "Name must be less than 30 characters"
    ],

    productDescription: "",
    productDescriptionRules: [
      v => !!v || "Product Description is required",
      v =>
        (v && v.length < 100) || "Description must be less than 100 characters"
    ],

    productPriceUsd: "",
    productPriceUsdRules: [
      v => !!v || "Product Price is required",
      v => (v && v > 0) || "Product price must be greater than zero"
    ],
    productPhoto: null,
    productPhotoRules: [v => !!v || "Product photo is required"],

    lazy: false
  }),
  methods: {
    sort(by) {
      switch (by) {
        case "price":
          this.products = this.products.sort(
            (a, b) => a.productPriceUsd - b.productPriceUsd
          );
          break;

        case "name":
          this.products = this.products.sort((a, b) =>
            a.productName.localeCompare(b.productName)
          );
          break;

        case "date":
          this.products = this.products.sort(
            (a, b) => a.dateCreated - b.dateCreated
          );
          break;

        default:
          break;
      }
    },
    addProductShowModal() {
      this.productId = null;
      this.uploadFile = true;
      this.clearForm();

      this.$modal.show("addOrEditProduct");
    },
    editProductShowModal(id) {
      this.productId = id;
      this.uploadFile = false;
      this.clearForm();

      this.products.forEach(prod => {
        if (id == prod.productId) {
          this.productId = id;
          this.productName = prod.productName;
          this.productDescription = prod.productDescription;
          this.productPriceUsd = prod.productPriceUsd;
          this.productFileName = prod.productFileName;
        }
        this.$modal.show("addOrEditProduct");
      });
    },

    removeProduct(id) {
      axios
        .get(`http://api.keter.sdf.co.il/api/Product/Delete?productId=${id}`)
        .then(res => {
          this.getProducts();
          this.$modal.hide("addOrEditProduct");
        })

        .catch(res => {
          console.log(res);
          this.validForm = true;
          alert("Error!");
        });
    },

    insertProduct() {
      if (!this.$refs.form.validate()) return false;

      let formData = new FormData();
      formData.append("productName", this.productName);
      formData.append("productDescription", this.productDescription);
      formData.append("productPriceUsd", this.productPriceUsd);
      formData.append("productPhoto", this.productPhoto);

      this.validForm = false;

      axios
        .post(
          "http://api.keter.sdf.co.il/api/Product/Insert",
          formData,
          this.$store.getters.getTokenHeaderFormData
        )
        .then(res => {
          this.getProducts();
          this.$modal.hide("addOrEditProduct");
          this.clearForm();
        })
        .catch(res => {
          console.log(res);
          this.validForm = true;
          alert("Error!");
        });
    },
    updateProduct() {
      if (!this.$refs.form.validate()) return false;

      let formData = new FormData();
      formData.append("productId", this.productId);
      formData.append("productName", this.productName);
      formData.append("productDescription", this.productDescription);
      formData.append("productPriceUsd", this.productPriceUsd);
      if (this.uploadFile) {
        formData.append("productPhoto", this.productPhoto);
      }

      this.validForm = false;

      axios
        .post(
          "http://api.keter.sdf.co.il/api/Product/update",
          formData,
          this.$store.getters.getTokenHeaderFormData
        )
        .then(res => {
          this.getProducts();
          this.$modal.hide("addOrEditProduct");
          this.clearForm();
        })
        .catch(res => {
          console.log(res);
          this.validForm = true;
          alert("Error!");
        });
    },

    getProducts() {
      axios
        .get("http://api.keter.sdf.co.il/api/Product/List")
        .then(res => {
          this.products = res.data;

          this.products.forEach(prod => {
            prod.dateCreated = moment()
              .add(prod.dateCreated, "days")
              .format("DD/MM/YYYY");

            prod.lastUpdated = moment()
              .add(prod.lastUpdated, "days")
              .format("DD/MM/YYYY");
          });
        })

        .catch(res => {
          console.log(res);
          this.validForm = true;
          alert("Error!");
        });
    },
    changeUploadFile() {
      this.uploadFile = !this.uploadFile;
    },

    clearForm() {
      this.productName = null;
      this.productDescription = null;
      this.productPriceUsd = null;
      this.productFileName = null;
      this.productPhoto = null;
    }
  },

  mounted() {
    this.getProducts();
  }
};
</script>


<style scoped>
.products {
  width: 100%;
}
.v-form {
  width: 100%;
}
.vm--modal {
    width: 80% !important;
    margin: auto;
    left: 0 !important;
    max-width: 580px;}
</style>