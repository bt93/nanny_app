<template>
  <div class="sessionLink">
        <router-link v-if="prevRoute === 'updateSession'" :to="{name: 'updateSession', params: {id: $route.params.id}}">Back to Session</router-link>
        <router-link v-else-if="prevRoute === 'endSession'" :to="{name: 'endSession', params: {id: $route.params.id}}">Back to End Session</router-link>
        <router-link v-else :to="{name: 'session', params: {id: $route.params.id}}">Back</router-link>
  </div>
</template>

<script>
export default {
    props: {
        prevRoute: String
    }
}
</script>

<style>

</style>