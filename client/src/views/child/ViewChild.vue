<template>
  <div class="viewChild">
      <img src="@/images/loading.gif" alt="Loading..." v-if="isLoading">
      <error v-else-if="error" />
      <div v-else>
          <router-link :to="{name: 'editChild'}"><h2>Edit</h2></router-link>
          <h1>{{ child.firstName }} {{ child.lastName }}</h1>
          <img :src="child.imageUrl" :alt="child.firstName" v-if="child.imageUrl" id="childImage">
          <ul>
              <li>Date of Birth: {{ child.dateOfBirth }}</li>
              <li>Gender: {{ getGender }}</li>
              <li>Needs Diapers? {{ getNeedsDiapers }}</li>
              <li>Rate Per Hour: ${{ child.ratePerHour }}</li>
              <li><h3>Parents / Guardian: </h3></li>
              <ul v-if="child.parents.length > 0">
                  <parent-container v-for="p in child.parents" :key="p.parentId" :parent="p" />
              </ul>
              <div v-else>
                  <h3>No Parents listed</h3>
              </div>
          </ul>
      </div>
  </div>
</template>

<script>
import childrenService from '@/services/ChildrenService'
import moment from 'moment'
import Error from '@/components/Error'
import ParentContainer from '@/components/ParentContainer'

export default {
    components: {
        Error,
        ParentContainer
    },
    data() {
        return {
            child: {},
            isLoading: true,
            error: false
        }
    },
    created() {
        childrenService.getChildById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.isLoading = false;
                    this.child = res.data;
                    let day = new Date(this.child.dateOfBirth);
                    this.child.dateOfBirth = moment(day).format("MM/DD/YYYY");
                }
            })
            .catch(err => {
                if (err) {
                    this.isLoading = false;
                    this.error = true;
                } 
            });
    },
    computed: {
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
    }
}
</script>

<style>
#newParent {
    margin: 30px;
}

#childImage {
    max-width: 800px;
    border-radius: 20px;
}
</style>