<template>
  <div id="app">
    <div id="nav">
      <div>
        <h2 id="title">Nanny Tracker</h2>
      </div>
      <div>
        <h1 v-if="$route.name === 'dashboard'">Dashboard</h1>
      </div>
      <div id="links">
        <span v-if="$store.state.token != ''"><router-link v-bind:to="{ name: 'dashboard' }">Home</router-link></span>
        <span v-if="$store.state.token != ''">&nbsp;|&nbsp;<router-link v-bind:to="{ name: 'settings' }">Settings</router-link>&nbsp;|&nbsp;</span>
        <span v-if="$store.state.token != ''"><router-link v-bind:to="{ name: 'logout' }">Logout</router-link></span>
      </div>
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
    display: flex;
    flex-wrap: nowrap;
    justify-content: space-between;
    align-items: center;
  }

  div > #nav > #links > span > a {
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

  .text-center > form > input,
  .text-center > form > select,
  .text-center > form > button {
    height: 30px;
    max-width: 580px;
    margin: auto;
  }
</style>
