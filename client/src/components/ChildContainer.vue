<template>
  <div class="childContainer">
      <h3>{{ child.firstName }} {{ child.lastName }}</h3>
      <img v-if="child.imageUrl" :src="child.imageUrl" :alt="child.firstName">
      <ul>
          <li>Date of Birth: {{ formatDOB }}</li>
          <li>Gender: {{ getGender }}</li>
          <li>Rate: ${{ child.ratePerHour }}</li>
          <li>Needs diapers: {{ getNeedsDiapers }}</li>
      </ul>
      <router-link :to="{name: 'editChild', params: {id: child.childId}}">Edit</router-link>
  </div>
</template>

<script>
export default {
    name: 'child-container',
    props: {
        child: Object
    },
    computed: {
        formatDOB() {
            const date = new Date(this.child.dateOfBirth);
            
            return `${date.getMonth() + 1}/${date.getDate()}/${date.getFullYear()}`
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