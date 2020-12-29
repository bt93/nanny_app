<template>
  <v-card>
    <v-card-title>{{ parent.firstName }} {{ parent.lastName }}</v-card-title>
    <v-card-text>Phone: {{ parent.phoneNumber }}</v-card-text>
    <v-card-text>Email: {{ parent.emailAddress }}</v-card-text>
    <v-card-title>Address:</v-card-title>
    <v-container>
      <h4>{{ parent.address.street }}</h4>
      <h4>{{ parent.address.city }}, {{ parent.address.state }} {{ parent.address.zip }}</h4>
      <h4 v-if="parent.address.county">{{ parent.address.county }} County</h4>
      <h4>{{ parent.address.country }}</h4>
    </v-container>

  </v-card>
</template>

<script>
export default {
    props: {
      parent: Object
    }
}
</script>