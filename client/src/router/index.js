import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Logout from '../views/Logout.vue'
import Register from '../views/Register.vue'
import Dashboard from '../views/Dashboard'
import Settings from '../views/Settings'
import SessionDetails from '../views/SessionDetails'
import NewSession from '../views/NewSession'
import NewChild from '../views/NewChild'
import EditChild from '../views/EditChild'
import store from '../store/index'

Vue.use(Router)

/**
 * The Vue Router is used to "direct" the browser to render a specific view component
 * inside of App.vue depending on the URL.
 *
 * It also is used to detect whether or not a route requires the user to have first authenticated.
 * If the user has not yet authenticated (and needs to) they are redirected to /login
 * If they have (or don't need to) they're allowed to go about their way.
 */

const router = new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      redirect: { name: 'dashboard' },
      meta: {
        requiresAuth: false
      }
    },
    {
      path: "/login",
      name: "login",
      component: Login,
      meta: {
        requiresAuth: false,
        title: 'Login'
      }
    },
    {
      path: "/logout",
      name: "logout",
      component: Logout,
      meta: {
        requiresAuth: false,
        title: 'Logout'
      }
    },
    {
      path: "/register",
      name: "register",
      component: Register,
      meta: {
        requiresAuth: false,
        title: 'Register'
      }
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      meta: {
        requiresAuth: true,
        title: 'Dashboard'
      }
    },
    {
      path: '/settings',
      name: 'settings',
      component: Settings,
      meta: {
        requiresAuth: true,
        title: 'User Settings'
      }
    },
    {
      path: '/session',
      name: 'newSession',
      component: NewSession,
      meta: {
        requiresAuth: true,
        title: 'New Session'
      }
    },
    {
      path: '/session/:id',
      name: 'session',
      component: SessionDetails,
      meta: {
        requiresAuth: true,
        title: 'Session'
      }
    },
    {
      path: '/child',
      name: 'newChild',
      component: NewChild,
      meta: {
        requiresAuth: true,
        title: 'Add Child'
      }
    },
    {
      path: '/child/:id',
      name: 'editChild',
      component: EditChild,
      meta: {
        requiresAuth: true,
        title: 'Edit Child'
      }
    }
  ]
})

router.beforeEach((to, from, next) => {
  // Determine if the route requires Authentication
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth);

  if (to.meta && to.meta.title) {
    document.title = to.meta.title + ' | Nanny Tracker';
  } else {
    document.title = 'Nanny Tracker'
  }

  // If it does and they are not logged in, send the user to "/login"
  if (requiresAuth && store.state.token === '') {
    next("/login");
  } else {
    // Else let them go to their next destination
    next();
  }
});

export default router;
