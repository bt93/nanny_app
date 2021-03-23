<template>
  <v-container class="editChild">
      <v-row v-if="isLoading" justify="center">
          <v-progress-circular 
            color="primary"
            indeterminate
          />
      </v-row>
      <error v-else-if="error"/>
      <v-card 
        elevation="2"
        v-else
      >
        <v-form
            @submit.prevent="updateChild"
            ref="form"
        >
            <v-container class="px-12">
                <v-row>
                    <v-col>
                        <v-text-field 
                            label="First Name"
                            v-model="child.firstName"
                            id="firstName"
                            :rules="nameRules"
                        />
                    </v-col>
                    <v-col>
                        <v-text-field 
                            label="Last Name"
                            v-model="child.lastName"
                            id="lastName"
                            :rules="nameRules"
                        />
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-select 
                            v-model="child.gender"
                            label="Gender"
                            :items="genders"
                            item-text="item"
                            item-value="value"
                        />
                    </v-col>
                    <v-col>
                       <v-text-field 
                            type="date"
                            label="Date Of Birth"
                            v-model="child.dateOfBirth"
                            id="dateOfBirth"
                        /> 
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-text-field 
                            type="number"
                            min="0" 
                            step="any"
                            label="Rate Per Hour (in U.S. Dollars)"
                            v-model="child.ratePerHour"
                            :rules="rateRules"
                        />
                    </v-col>
                    <v-col>
                        <v-radio-group
                            v-model="child.needsDiapers"
                            label="Needs Diaper?"
                            row
                            :rules="diaperRules"
                        >
                            <v-radio 
                                label="Yes"
                                :value="true"
                            />
                            <v-radio 
                                label="No"
                                :value="false"
                            />
                        </v-radio-group>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-container v-if="child.imageUrl && !newImage" class="d-flex justify-center">
                            <v-img 
                                
                                :src="child.imageUrl"
                                :alt="child.firstName"
                                max-width="20vh"
                            />
                        </v-container>
                        <upload-photo @image-upload="imageUpload" v-else-if="!child.imageUrl || newImage" />
                    </v-col>
                </v-row>
                <v-row justify="center">
                    <v-btn @click="changeNewImage">{{ (newImage) ? 'Cancel' : 'Update Image' }}</v-btn>
                </v-row>
                <v-row justify="center">
                    <v-col>
                        <add-parent :child="child" />
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <new-parent />
                    </v-col>
                </v-row>
                <v-row  class="justify-center">
                    <v-list-item v-if="child.allergies.length > 0" class="justify-center">
                        Allergies:
                        <v-list>
                            <v-list-item v-for="allergy in child.allergies" :key="allergy.allergyId">{{ allergy.name }} <span class="pl-4" @click="removeAllergyFromChild(allergy)">❌</span></v-list-item>
                        </v-list>
                    </v-list-item>
                </v-row>
                <v-row justify="center">
                    <v-col>
                        <add-allergy :childId="child.childId"/>
                    </v-col>
                </v-row>
                <v-row justify="center">
                    <v-btn class="mr-6" type="submit">Submit</v-btn>
                    <v-btn :to="{name: 'viewChild', parms: {id: child.childId}}">Cancel</v-btn>
                </v-row>
            </v-container>
        </v-form>  
      </v-card>
  </v-container>
</template>

<script>
import childrenService from '@/services/ChildrenService'
import Error from '@/components/Error'
import moment from 'moment'
import UploadPhoto from '@/components/UploadPhoto.vue'
import AddParent from '@/components/AddParent.vue'
import NewParent from '../../components/NewParent.vue'
import AddAllergy from '@/components/AddAllergy'

export default {
    components: {
        Error,
        UploadPhoto,
        AddParent,
        NewParent,
        AddAllergy
    },
    data() {
        return {
            child: {},
            genders: [
                { item: 'Female', value: 'F' },
                { item: 'Male', value: 'M' },
                { item: 'Non-Binary', value: 'N' },
                { item: 'Other', value: 'O' }
            ],
            isLoading: true,
            error: false,
            newImage: false,
            nameRules: [
                v => !!v || 'Name is required'
            ],
            rateRules: [
                v => !!v || 'Rate Per Hour is required'
            ],
            diaperRules: [
                v => !!v || 'Needs Diapers is required'
            ]
        }
    },
    methods: {
        updateChild() {
            this.child.ratePerHour = Number(this.child.ratePerHour);

            if (this.$refs.form.validate()) {
                childrenService.updateChild(this.child)
                .then(res => {
                    if (res.status === 201) {
                        this.$router.push({name: 'dashboard'});
                    }
                })
                .catch(err => {
                    if (err) {
                        this.error = true;
                    }
                });
            }
            
        },

        changeNewImage() {
            (this.newImage) ? this.newImage = false : this.newImage = true;
        },

        imageUpload(value) {
            this.child.imageUrl = value;
        },
        
        removeAllergyFromChild(allergy) {
            if (confirm(`Are you sure you'd like to remove ${allergy.name} from ${this.child.firstName}'s Allergy list?`)) {
                childrenService.removeAllergyFromChild(this.child.childId, allergy.allergyId)
                    .then(res => {
                        if (res.status === 204) {
                            window.location.reload();
                        }
                    })
            }
        }
    },
    created() {
        childrenService.getChildById(this.$route.params.id)
            .then(res => {
                if (res.status === 200) {
                    this.isLoading = false;
                    this.child = res.data;
                    let day = new Date(this.child.dateOfBirth);
                    this.child.dateOfBirth = moment(day).format("YYYY-MM-DD");
                }
            })
            .catch(err => {
                if (err) {
                    this.isLoading = false;
                    this.error = true;
                } 
            });
    }
}
</script>

<style>

</style>