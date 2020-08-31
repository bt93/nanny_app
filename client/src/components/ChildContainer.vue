<template>
  <div class="childContainer">
      <router-link :to="{name: 'viewChild', params: {id: child.childId}}"><h3>{{ child.firstName }} {{ child.lastName }}</h3></router-link>
      <img v-if="child.imageUrl" :src="child.imageUrl" :alt="child.firstName">
      <ul>
          <li>Date of Birth: {{ formatDOB }}</li>
          <li>Gender: {{ getGender }}</li>
          <li>Rate: ${{ child.ratePerHour }}</li>
          <li>Needs diapers: {{ getNeedsDiapers }}</li>
      </ul>
  </div>
</template>

<script>
import moment from 'moment'

export default {
    name: 'child-container',
    props: {
        child: Object
    },
    computed: {
        formatDOB() {
            const day = new Date(this.child.dateOfBirth);
            
            return moment(day).format("MM/DD/YYYY")
        },
        getGender() {
            if (this.child.gender === 'F') {
                return 'Female';
            } else if (this.child.gender === 'M') {
                return 'Male'
            } else if (this.child.gender === 'N') {
                return 'Non-Binary'
            }
            
            return 'Other'
        },
        getNeedsDiapers() {
            if (this.child.needsDiapers) {
                return 'Yes'
            } else {
                return 'No'
            }
        }
    },
}
</script>

<style>
ul {
    list-style: none;
    padding: 0;
}
</style>