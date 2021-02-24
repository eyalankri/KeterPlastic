<template>
  <nav>
    <v-app-bar app clipped-right color="blue-grey" dark>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>
        <span class="text-uppercase grey--text text--lighten-2">Gift</span> Store
      </v-toolbar-title>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" app color="grey lighten-4">
      <v-list dense>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="title">Menu</v-list-item-title>
            <v-list-item-subtitle>Please select one of the pages</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
        <v-divider></v-divider>

        <v-list-item v-for="(item, i) in items" :key="i"  :to="item.link" link>
          <v-list-item-action>
            <v-icon>{{item.icon}}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{item.text}}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

 
<script>
export default {
  data: () => ({
    drawer: null,
    drawerRight: null,
    right: false,
    left: false,
    items: [
      {
        icon: "mdi-exit-to-app",
        text: "Products List",
        link: "/"
      },
       
    ]
  }),
};
</script>

