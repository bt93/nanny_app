<template>
  <div class="sessionContainer">
    <h3>{{ session.child.firstName }} {{ session.child.lastName }}</h3>
    <p>Rate: {{ session.child.ratePerHour }}</p>
    <p>Drop off: {{ formatDropOff }}</p>
    <p>Pick up: {{ formatPickUp }}</p>
    <router-link :to="{name: 'login'}">More Details</router-link>
  </div>
</template>

<script>
export default {
  name: 'session-container',
  props: {
    session: Object
  },
  computed: {
    formatDropOff() {
      const date = new Date(this.session.dropOff)
      let amPm;
      if (date.getHours() <= 12) {
        amPm = 'AM'
      } else {
        amPm = 'PM'
      }
      return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()} - ${date.getHours()}:${date.getMinutes()}${amPm}`
    },
    formatPickUp() {
      if (this.session.pickUp === null) {
        return 'N/A'
      }

      const date = new Date(this.session.formatPickUp)
      let amPm;
      if (date.getHours() <= 12) {
        amPm = 'AM'
      } else {
        amPm = 'PM'
      }
      return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()} - ${date.getHours()}:${date.getMinutes()}${amPm}`
    }
  }
}
</script>

<style>
.sessionContainer {
  border: black 10px solid;
}
</style>