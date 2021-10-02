<template>
  <v-container class="settings">
    <v-row v-if="isLoading" justify="center">
          <v-progress-circular 
            color="primary"
            indeterminate
          />
      </v-row>
    <Error v-else-if="error" />
    <v-container v-else>
      <v-form @submit.prevent="submit" ref="form">
        <v-container class="d-md-flex justify-space-around mb-12">
          <v-card elevation="2" class="px-6">
            <h2>Basic Info</h2>
            <v-row>
              <v-col>
                <v-text-field 
                  label="First Name"
                  id="firstName"
                  v-model="caretaker.firstName"
                  :rules="nameRules"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="Last Name"
                  id="lastName"
                  v-model="caretaker.lastName"
                  :rules="nameRules"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Email Address"
                  id="emailAddress"
                  v-model="caretaker.emailAddress"
                  :rules="emailRules"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Phone Number"
                  type="tel"
                  id="phoneNumber"
                  v-model="caretaker.phoneNumber"
                />
              </v-col>
            </v-row>
          </v-card>
          <v-card elevation="2" class="px-6">
            <h2>Address</h2>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Street"
                  id="street"
                  v-model="caretaker.address.street"
                  :rules="streetRules"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="City"
                  id="city"
                  v-model="caretaker.address.city"
                  :rules="cityRules"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="state"
                  id="state"
                  v-model="caretaker.address.state"
                  :rules="stateRules"
                />
              </v-col>
            </v-row>
            <v-row>
              <v-col>
                <v-text-field 
                  label="Zip Code"
                  id="zip"
                  v-model="caretaker.address.zip"
                />
              </v-col>
              <v-col>
                <v-text-field 
                  label="County"
                  id="county"
                  v-model="caretaker.address.county"
                />
              </v-col>
            </v-row>
          </v-card>
        </v-container>
        <v-row justify="center">
          <v-btn type="submit" class="mr-6">Submit</v-btn>
          <v-btn :to="{ name: 'changePassword' }" class="mr-6" color="error">Change Password</v-btn>
          <v-btn :to="{ name: 'dashboard' }">Cancel</v-btn>
        </v-row>
      </v-form>
    </v-container>
  </v-container>
</template>

<script>
import caretakerService from '../services/CaretakerService'
import Error from '../components/Error'

export default {
  components: {
    Error
  },
  data() {
    return {
      caretaker: {},
      isLoading: true,
      error: false,
      nameRules: [
        v => !!v || 'Field is required',
      ],
      emailRules: [
        v => !!v || 'Email Address is required',
        v => /.+@.+/.test(v) || 'Email Address must be valid'
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
    submit() {
      if (this.$refs.form.validate()) {
        this.isLoading = true;
        caretakerService.updateCaretaker(this.caretaker)
          .then(res => {
            if (res.status === 201) {
              this.$router.push('/dashboard');
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
  },
  created() {
    caretakerService.getCaretaker()
      .then(res => {
        if (res.status === 200) {
          this.caretaker = res.data;
          this.isLoading = false;
        }
      })
      .catch(err => {
        console.log(err);
        this.isLoading = false;
        this.error = true;
      })
  }
}
</script>

<style>
.settings{
  display: block;
}

div {
  margin-bottom: 10px;
}
</style>