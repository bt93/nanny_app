<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <main>
      <router-view />
    </main>
  </div>
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=Ubuntu:ital,wght@0,300;0,700;1,400&display=swap');

  * {
    box-sizing: border-box;
  }

  body {
    font-family: 'Ubuntu', sans-serif;
    min-height: 100vh;
    position: relative;
    margin: 0;
  }

  main {
    margin: 0 10% 0 10%;
    text-align: center;
  }

  div > #nav {
    border-bottom: 2px rgb(37, 57, 73) solid;
    background-color: rgb(58, 81, 102);
    padding: 20px;
  }

  div > #nav > a {
    color: rgb(72, 223, 185);
    text-decoration: none;
  }

  .text-center {
    text-align: center;
  }

  .text-center > form {
    display: flex;
    flex-direction: column;
    margin: 0 20% 0 20%;
  }

  .text-center > form > div {
    
  }
</style>
