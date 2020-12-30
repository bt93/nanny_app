<template>
  <v-container class="viewChild">
        <v-row justify="center" class="my-9" v-if="isLoading">
            <v-progress-circular 
              color="primary" 
              indeterminate 
              class="py-4"
            ></v-progress-circular>
        </v-row>
      <error v-else-if="error" />
      <v-container v-else>
          <v-row>
              <v-col
                cols="12"
              >
                <v-card>
                    <v-card-title class="justify-center">{{ child.firstName }} {{ child.lastName }}</v-card-title>
                    <v-card-actions class="justify-center">
                        <v-img
                            :src="child.imageUrl"
                            :alt="child.firstName"
                            max-width="20vh"
                        />
                    </v-card-actions>
                    <v-card-actions class="justify-center">
                        <v-list>
                            <v-list-item>Date of Birth: {{ child.dateOfBirth }}</v-list-item>
                            <v-list-item>Gender: {{ getGender }}</v-list-item>
                            <v-list-item>Needs Diapers? {{ getNeedsDiapers }}</v-list-item>
                            <v-list-item>Rate Per Hour: ${{ child.ratePerHour }}</v-list-item>
                        </v-list>
                    </v-card-actions>
                    <v-row class="pb-2" justify="center">
                        <v-btn :to="{name: 'editChild', params: {id: child.childId}}">Edit {{ child.firstName }}</v-btn>
                    </v-row>
                </v-card>
              </v-col>
          </v-row>
          <v-row justify="center" v-if="child.parents.length > 0">
              <h1>Parents/Gardians</h1>
          </v-row>
          <v-row v-if="child.parents.length > 0">
              <v-col
                cols="12"
                md="6"
                v-for="parent in child.parents"
                :key="parent.parentId"
              >
                 <parent-container :parent="parent"/>
              </v-col>
          </v-row>
      </v-container>
  </v-container>
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

.parents {
    display: flex;
    justify-content: space-around;
}

#childImage {
    max-width: 300px;
    border-radius: 20px;
}
</style>