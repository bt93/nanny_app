<template>
  <v-card
    elevation="2"
    class="mx-12 px-12"
  >
    <v-form class="mx-12" 
    @submit.prevent="login" 
    v-model="valid"
    ref="form">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <v-container>
        <v-row v-if="invalidCredentials">
          <h3>Invalid Email and Password</h3>
        </v-row>
        <v-row>
          <v-text-field
            label="Email Address"
            v-model="user.emailAddress"
            id="emailAddress"
            required
            :rules="emailRules"
          ></v-text-field>
        </v-row>
        <v-row>
          <v-text-field
            label="Password"
            v-model="user.password"
            id="password"
            type="password"
            required
            :rules="passwordRules"
          ></v-text-field>
        </v-row>
        <v-row justify="center">
          <router-link :to="{ name: 'register' }">Need an account?</router-link>
        </v-row>
        <v-row justify="center" class="my-9">
            <v-progress-circular 
              color="primary" 
              indeterminate 
              v-if="isLoading"
              class="py-4"
            ></v-progress-circular>
            <div v-else class="py-4"></div>
        </v-row>
        <v-row justify="center">
          <v-btn type="submit">Sign in</v-btn>
        </v-row>
      </v-container>
    </v-form>
  </v-card>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  data() {
    return {
      user: {
        emailAddress: "",
        password: ""
      },
      invalidCredentials: false,
      isLoading: false,
      valid: false,
      emailRules: [
        v => !!v || 'Email Address is required',
        v => /.+@.+/.test(v) || 'Email Address must be valid'
      ],
      passwordRules: [
        v => !!v || 'Password is required',
      ]
    };
  },
  methods: {
    login() {
      if (this.$refs.form.validate()) {
        this.isLoading = true;
        authService
        .login(this.user)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data);
            this.$router.push("/dashboard");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.isLoading = false;
            this.invalidCredentials = true;
          }
        }); 
      }
    }
  }
};
</script>
