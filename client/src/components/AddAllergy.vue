<template>
  <v-container>
      <v-row justify="center">
        <v-btn @click="overlay = true">Add Allergy</v-btn>  
      </v-row>
      <v-overlay :value="overlay">
          <v-card class="pa-12">
              <v-row justify="center" class="pt-6">
                  <v-btn @click="overlay = false">Back</v-btn>
              </v-row>
              <v-row>
                  <v-form @submit.prevent="submitAllergy">
                      <v-row>
                            <v-select 
                            label="Allergy Type"
                            :items="allergyTypes"
                            item-text="name"
                            item-value="allergyTypeId"
                            v-model="currentAllergyTypeId"
                            @change="getAllergiesByTypeId"
                            />  
                      </v-row>
                      <v-row>
                          <v-select 
                            label="Allergy"
                            :items="allergies"
                            item-text="name"
                            item-value="allergyId"
                            v-model="selectedAllergyId"
                          />
                      </v-row>
                      <v-row justify="center">
                          <p>Don't see an allergy listed here that you'd like to add? Fill out <a href="https://forms.gle/rkKq7Pho7WEbSSxDA" target="_blank">this form</a> to add it!</p>
                      </v-row>
                      <v-row justify="center" class="pt-6">
                          <v-btn type="submit">Submit</v-btn>
                      </v-row>
                  </v-form>
              </v-row>
          </v-card>
      </v-overlay>
  </v-container>
</template>

<script>
import childrenService from '@/services/ChildrenService'

export default {
    data() {
        return {
            allergies: [],
            allergyTypes: [],
            currentAllergyTypeId: 1,
            overlay: false,
            selectedAllergyId: null
        }
    },
    props: {
        childId: Number
    },
    created() {
        childrenService.getAllergyTypes()
            .then(res => {
                if (res.status === 200) {
                    this.allergyTypes = res.data;
                }
            });
        this.getAllergiesByTypeId();
    },
    methods: {
        getAllergiesByTypeId() {
            childrenService.getAllergiesByAllergyTypeId(this.currentAllergyTypeId)
                .then(res => {
                    if (res.status === 200) {
                        this.allergies = res.data;
                    }
                })
        },

        submitAllergy() {
            childrenService.addAllergyToChild(this.childId, this.selectedAllergyId)
                .then(res => {
                    if (res.status === 201) {
                        this.overlay = false;
                    }
                })
        }
    }
}
</script>