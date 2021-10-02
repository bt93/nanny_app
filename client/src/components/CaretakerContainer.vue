<template>
  <v-container>
    <v-card-text>Email: {{ caretaker.emailAddress }}</v-card-text>
    <v-card-text>Phone Number: {{ caretaker.phoneNumber }}</v-card-text>
    <v-card-text class="">
        <h2>Address</h2>
        <h4>{{ caretaker.address.street }}</h4>
        <h4>{{ caretaker.address.city }}, {{ caretaker.address.state }} {{ caretaker.address.zip }}</h4>
        <h4 v-if="caretaker.address.county">{{ caretaker.address.county }} County</h4>
        <h4>{{ caretaker.address.country }}</h4>
    </v-card-text>
  </v-container>
</template>

<script>
export default {
    props: {
        caretaker: Object
    }
}
</script>

