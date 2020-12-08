import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
import jwtDecode from 'jwt-decode'
import moment from 'moment'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */
let currentToken = localStorage.getItem('token')
let currentUser = JSON.parse(localStorage.getItem('user'));

if(currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;

  let exp = jwtDecode(currentToken).exp;
  if (moment.unix(exp) < moment()) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      currentToken = '';
      currentUser = {};
      axios.defaults.headers.common = {};
  }
}

export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    cloudinaryUploadUrl: process.env.VUE_APP_CLOUDINARY_UPLOAD_URL,
    cloudinaryAPIKey: process.env.VUE_APP_CLOUDINARY_API_KEY,
    cloudinaryUploadPreset: process.env.VUE_APP_CLOUDINARY_UPLOAD_PRESET
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user',JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    }
  }
})
