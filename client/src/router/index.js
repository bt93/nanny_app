import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/views/Home.vue'
import Login from '@/views/Login.vue'
import Logout from '@/views/Logout.vue'
import Register from '@/views/Register.vue'
import Dashboard from '@/views/Dashboard'
import Settings from '@/views/Settings'
import ChangePassword from '@/views/ChangePassword'
import AllSessions from '@/views/sessions/AllSessions'
import SessionDetails from '@/views/sessions/SessionDetails'
import NewSession from '@/views/sessions/NewSession'
import UpdateSession from '@/views/sessions/UpdateSession'
import EndSession from '@/views/sessions/EndSession'
import DeleteSession from '@/views/sessions/DeleteSession'
import NewChild from '@/views/child/NewChild'
import ViewChild from '@/views/child/ViewChild'
import EditChild from '@/views/child/EditChild'
import store from '@/store/index'
import VueAnalytics from 'vue-analytics'

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
      path: '/password',
      name: 'changePassword',
      component: ChangePassword,
      meta: {
        requiresAuth: true,
        title: 'Change Password'
      }
    },
    {
      path: '/session',
      name: 'allSessions',
      component: AllSessions,
      meta: {
        requiresAuth: true,
        title: 'All Sessions'
      }
    },
    {
      path: '/session/new',
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
      path: '/session/:id/update',
      name: 'updateSession',
      component: UpdateSession,
      meta: {
        requiresAuth: true,
        title: 'Update Session'
      }
    },
    {
      path: '/session/:id/end',
      name: 'endSession',
      component: EndSession,
      meta: {
        requiresAuth: true,
        title: 'End Session'
      }
    },
    {
      path: '/session/delete',
      name: 'deleteSession',
      component: DeleteSession,
      meta: {
        requiresAuth: true,
        title: 'Delete Session'
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
      name: 'viewChild',
      component: ViewChild,
      meta: {
        requiresAuth: true,
        title: 'Child'
      }
    },
    {
      path: '/child/:id/edit',
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

Vue.use(VueAnalytics, {
  id: process.env.VUE_APP_GOOGLE_ANALYTICS_ID.toString(),
  router,
  debug: {
    sendHitTask: process.env.NODE_ENV === 'production'
  }
});

export default router;
