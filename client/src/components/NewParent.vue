<template>
  <v-container class="newParent text-center">
      <v-btn @click="overlay = true">Add New Parent</v-btn>
      <v-overlay :value="overlay">
          <v-card class="px-12">
            <v-form @submit.prevent="createParent" ref="form">
                <v-row>
                    <v-col>
                        <h3>Add a new Parent</h3>
                        <h4>Basic Info</h4>
                        <v-row>
                            <v-col>
                                <v-text-field
                                    id="firstName"
                                    label="First Name"
                                    v-model="parent.firstName"
                                    :rules="nameRules"
                                />
                            </v-col>
                            <v-col>
                                <v-text-field
                                    id="lastName"
                                    label="Last Name"
                                    v-model="parent.lastName"
                                    :rules="nameRules"
                                />
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-text-field
                                        id="emailAddress"
                                        label="Email Address"
                                        v-model="parent.emailAddress"
                                        :rules="emailRules"
                                    />
                            </v-col>
                            <v-col>
                                <v-text-field
                                        id="phoneNumber"
                                        label="Phone Number"
                                        v-model="parent.phoneNumber"
                                        :rules="phoneNumberRules"
                                        type="tel"
                                    />
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <h4>Address</h4>
                        <v-row>
                            <v-col>
                                <v-text-field
                                    id="street"
                                    label="Street Name"
                                    v-model="parent.address.street"
                                    :rules="streetRules"
                                /> 
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-text-field
                                    id="city"
                                    label="City"
                                    v-model="parent.address.city"
                                    :rules="cityRules"
                                />
                            </v-col>
                            <v-col>
                                <v-select
                                    id="state"
                                    label="State"
                                    v-model="parent.address.state"
                                    required
                                    :rules="stateRules"
                                    :items="states"
                                    item-text="name"
                                    item-value="name"
                                />
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                                <v-text-field
                                    id="zip"
                                    label="Zip Code"
                                    v-model="parent.address.zip"
                                />
                            </v-col>
                            <v-col>
                                <v-text-field
                                    id="county"
                                    label="County"
                                    v-model="parent.address.county"
                                />
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col>
                            <v-select
                                id="country"
                                label="Country"
                                :rules="countryRules"
                                v-model="parent.address.country"
                                :items="countries"
                            ></v-select>
                            </v-col>
                        </v-row>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col>
                        <v-btn type="submit">Add Parent</v-btn>
                    </v-col>
                    <v-col>
                        <v-btn @click="overlay = false">Cancel</v-btn>
                    </v-col>
                </v-row>
            </v-form>  
          </v-card>
      </v-overlay>
      
  </v-container>
</template>

<script>
import countryList from 'country-list';
import parentService from '../services/ParentService'
import statesList from '../services/States.json'

export default {
    name: 'new-parent',
    data() {
        return {
            parent: {
                firstName: '',
                lastName: '',
                emailAddress: '',
                phoneNumber: '',
                address: {
                    street: '',
                    city: '',
                    state: '',
                    zip: '',
                    county: '',
                    country: ''
                }
            },
            countries: countryList.getNames(),
            error: false,
            states: [],
            overlay: false,
            nameRules: [
                v => !!v || 'Field is required',
            ],
            emailRules: [
                v => !!v || 'Email Address is required',
                v => /.+@.+/.test(v) || 'Email Address must be valid'
            ],
            phoneNumberRules: [
                v => !!v || 'Phone Number is required',
                v => /^[2-9]\d{2}\d{3}\d{4}$/.test(v) || 'Phone number must be in the following format: XXX-XXX-XXXX'
            ],
            streetRules: [
                v => !!v || 'Street Name is required'
            ],
            cityRules: [
                v => !!v || 'City Name is required'
            ],
            stateRules: [
                v => !!v || 'State is required'
            ],
            countryRules: [
                v => !!v || 'Country is required'
            ],
        }
    },
    methods: {
        createParent() {
            if (this.$refs.form.validate()) {
              this.parent.address.zip = parseInt(this.parent.address.zip);

                parentService.addParent(this.parent, this.$route.params.id)
                    .then(res => {
                        if (res.status === 201) {
                            this.overlay = false;
                            this.$emit('new-parent', this.parent);
                        }
                    })
                    .catch(err => {
                        if (err) {
                            this.error = true;
                            console.log(err);
                        }
                    });  
            }
        }
    },
    created() {
        this.states = statesList;
    }
}
</script>

<style>

</style>