<template>
  <li class="parent">
    <h5>{{ parent.firstName }} {{ parent.lastName }}</h5>
    <ul>
      <li>Phone: {{ parent.phoneNumber }}</li>
      <li>Email: {{ parent.emailAddress }}</li>
      <li><h5>Address: </h5></li>
      <li id="address">
        <p>{{ parent.address.street }}</p>
        <p>{{ parent.address.city }}, {{ parent.address.state }} {{ parent.address.zip }}</p>
        <p v-if="parent.address.county">{{ parent.address.county }} County</p>
        <p>{{ parent.address.country }}</p>
      </li>
    </ul>
  </li>
</template>

<script>
export default {
    props: {
      parent: Object
    }
}
</script>

<style>
#address > p {
  margin: 0;
}
</style>