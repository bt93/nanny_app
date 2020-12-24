<template>
  <div id="register" class="text-center">
    <v-form class="form-register" @submit.prevent="register" ref="form">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <v-container class="d-md-flex justify-space-around">
        <v-card elevation="2" class="px-6">
          <h2>Basic Info</h2>
          <v-row>
            <v-col
              cols="6"
            >
              <v-text-field
              id="firstName"
              label="First Name"
              v-model="user.firstName"
              :rules="nameRules"
              ></v-text-field>
            </v-col>
            <v-col
              cols="6"
            >
              <v-text-field
                id="lastName"
                label="Last Name"
                v-model="user.lastName"
                :rules="nameRules"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-text-field
                id="emailAddress"
                label="Email Address"
                v-model="user.emailAddress"
                :rules="emailRules"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col
              cols="6"
            >
            <v-text-field
                id="password"
                label="Password"
                v-model="user.password"
                :rules="passwordRules"
                type="password"
              ></v-text-field>
            </v-col>
            <v-col
              cols="6"
            >
              <v-text-field
                id="confirmPassword"
                label="Confirm Password"
                v-model="user.confirmPassword"
                :rules="confirmPasswordRules"
                type="password"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-text-field
                id="phoneNumber"
                label="Phone Number"
                v-model="user.phoneNumber"
                :rules="phoneNumberRules"
                type="tel"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card>
        <v-card elevation="2" class="px-6">
          <h2>Address</h2>
          <v-row>
            <v-col>
              <v-text-field
                id="street"
                label="Street Name"
                v-model="user.address.street"
                :rules="streetRules"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-text-field
                id="city"
                label="City"
                v-model="user.address.city"
                :rules="cityRules"
              ></v-text-field>
            </v-col>
            <v-col>
              <v-select
                id="state"
                label="State"
                v-model="user.address.state"
                required
                :rules="stateRules"
                :items="states"
                item-text="name"
                item-value="name"
              >
              </v-select>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-text-field
                id="zip"
                label="Zip Code"
                v-model="user.address.zip"
              ></v-text-field>
            </v-col>
            <v-col>
              <v-text-field
                id="county"
                label="County"
                v-model="user.address.county"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-select
                id="country"
                label="Country"
                :rules="countryRules"
                v-model="user.address.country"
                :items="countries"
              ></v-select>
            </v-col>
          </v-row>
        </v-card>
      </v-container>
      <v-container class="mx-auto" justify="center">
        <v-row>
         <v-col>
            <router-link :to="{ name: 'login' }">Have an account?</router-link>
         </v-col>
        </v-row>
        <v-row justify="center">
        <v-progress-circular 
          color="primary" 
          indeterminate 
          v-if="isLoading"
          class="py-4"
        ></v-progress-circular>
        <v-row
          class="mx-auto"
          v-else-if="registrationErrors"
        >
          <v-col>
            <h2 class="text-xl red--text mx-auto">{{ registrationErrorMsg }}</h2>
            <h2 class="text-lg red--text mx-auto">{{ errorMsgs }}</h2>
          </v-col>
        </v-row>
        <div v-else class="py-4"></div>
        </v-row>
        <v-row>
          <v-col>
            <v-btn type="submit">Create Account</v-btn>
          </v-col>
        </v-row>
      </v-container>
    </v-form>
  </div>
</template>

<script>
import authService from '../services/AuthService';
import countryList from 'country-list';
import statesList from '../services/States.json'

export default {
  name: 'register',
  data() {
    return {
      user: {
        firstName: '',
        lastName: '',
        emailAddress: '',
        password: '',
        phoneNumber: '',
        confirmPassword: '',
        address: {
          street: '',
          city: '',
          state: '',
          zip: '',
          county: '',
          country: ''
        }
      },
      nameRules: [
        v => !!v || 'Field is required',
      ],
      passwordRules: [
        v => !!v || 'Password is required'
      ],
      confirmPasswordRules: [
        v => !!v || 'Confirm Password is required',
        v => v === this.user.password || 'Password must match'
      ],
      emailRules: [
        v => !!v || 'Email Address is required',
        v => /.+@.+/.test(v) || 'Email Address must be valid'
      ],
      phoneNumberRules: [
        v => !!v || 'Phone Number is required',
        v => /^[2-9]\d{2}-\d{3}-\d{4}$/.test(v) || 'Phone number must be in the following format: XXX-XXX-XXXX'
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
      countries: countryList.getNames(),
      states: [],
      isLoading: false,
      registrationErrors: false,
      registrationErrorMsg: '',
      errorMsgs: ''
    };
  },
  methods: {
    register() {
      if (this.$refs.form.validate()) {
      this.isLoading = true
      this.user.address.zip = parseInt(this.user.address.zip);
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.login()
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            this.isLoading = false;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Oops there were some Validation Errors';
              this.errorMsgs = response.data.errors
            }

            if (response.status === 409) {
              this.registrationErrorMsg = 'There were problems registering this user.';
              this.errorMsgs = response.data.message;
            }
          });
      }
    },
    login() {
      authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.emailAddress);
            this.$router.push("/dashboard");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
  created() {
    this.states = statesList;
  }
};
</script>

<style></style>
