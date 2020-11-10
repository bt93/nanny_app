<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p v-for="e in errorMsgs.FirstName" :key="e">{{ e }}</p>
      </div>
      <label for="firstName" class="sr-only">First Name</label>
      <input
        type="text"
        id="firstName"
        class="form-control"
        placeholder="First Name"
        v-model="user.firstName"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p v-for="e in errorMsgs.LastName" :key="e">{{ e }}</p>
      </div>
      <label for="lastName" class="sr-only">Last Name</label>
      <input
        type="text"
        id="lastName"
        class="form-control"
        placeholder="Last Name"
        v-model="user.lastName"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p v-for="e in errorMsgs.EmailAddress" :key="e">{{ e }}</p>
      </div>
      <label for="emailAddress" class="sr-only">Email</label>
      <input
        type="email"
        id="emailAddress"
        class="form-control"
        placeholder="Email Address"
        v-model="user.emailAddress"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p v-for="e in errorMsgs.Password" :key="e">{{ e }}</p>
      </div>
      <label for="password" class="sr-only">Password</label>
      <input
        type="password"
        id="password"
        class="form-control"
        placeholder="Password"
        v-model="user.password"

      />
      <input
        type="password"
        id="confirmPassword"
        class="form-control"
        placeholder="Confirm Password"
        v-model="user.confirmPassword"

      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p v-for="e in errorMsgs.PhoneNumber" :key="e">{{ e }}</p>
      </div>
      <label for="phoneNumber" class="sr-only">Phone Number</label>
      <input
        type="tel"
        id="phoneNumber"
        class="form-control"
        placeholder="Phone Number"
        v-model="user.phoneNumber"
        
        autofocus
      />
      <h2>Address</h2>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p>{{ errorMsgs["Address.Street"][0] }}</p>
      </div>
      <label for="street" class="sr-only">Street</label>
      <input
        type="street"
        id="street"
        class="form-control"
        placeholder="Street"
        v-model="user.address.street"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p>{{ errorMsgs["Address.City"][0] }}</p>
      </div>
      <label for="city" class="sr-only">City</label>
      <input
        type="city"
        id="city"
        class="form-control"
        placeholder="City"
        v-model="user.address.city"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p>{{ errorMsgs["Address.State"][0] }}</p>
      </div>
      <label for="state" class="sr-only">State</label>
      <input
        type="text"
        id="state"
        class="form-control"
        placeholder="State"
        v-model="user.address.state"

        autofocus
      />
      <label for="zip" class="sr-only">Zip Code</label>
      <input
        type="number"
        id="zip"
        min="0"
        max="99999"
        class="form-control"
        placeholder="Zip Code"
        v-model="user.address.zip"
        inputmode="numeric" pattern="[0-9]*"

        autofocus
      />
      <label for="county" class="sr-only">County</label>
      <input
        type="text"
        id="county"
        class="form-control"
        placeholder="County"
        v-model="user.address.county"
        autofocus
      />
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        <p>{{ errorMsgs["Address.Country"][0] }}</p>
      </div>
      <label for="country" class="sr-only">Country</label>
      <select name="country" id="country" v-model="user.address.country">
        <option value="" disabled selected="selected">Choose One</option>
        <option v-for="co in countries" :key="co" :value="co">{{ co }}</option>
      </select>
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
      <button class="btn btn-lg btn-primary btn-block" type="submit">
        Create Account
      </button>
    </form>
  </div>
</template>

<script>
import authService from '../services/AuthService';
import countryList from 'country-list';



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
      errorMsgs: {},
      countries: countryList.getNames(),
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
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
            if (response.status === 400) {
              this.registrationErrorMsg = 'Oops there were some Validation Errors';
              this.errorMsgs = response.data.errors
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
};
</script>

<style></style>
