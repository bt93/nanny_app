<template>
  <div>
    <ul class="options">
        <li><router-link :to="{name: 'endSession'}">End Session</router-link></li>
        <li><router-link :to="{name: 'addNap'}">Add Nap</router-link></li>
        <li><router-link :to="{name: 'addMeal'}">Add Meal</router-link></li>
        <li><router-link :to="{name: 'addDiaper'}">Add Diaper</router-link></li>
        <li><router-link id="delete" :to="{name: 'deleteSession', params: {id: sessionId} }">Delete Session</router-link></li>
    </ul>
  </div>
</template>

<script>
export default {
    props: {
        sessionId: Number
    }
}
</script>

<style>
.options {
    list-style: none;
    margin: 20px;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
}

.options > li {
    margin: 20px;
}

.options > li > a {
    text-decoration: none;
    color: rgb(58, 81, 102);
    border: rgb(58, 81, 102) 2px solid;
    padding: 5px;
    border-radius: 20px;
}

.options > li > a:hover {
    background-color: rgb(58, 81, 102);
    color: white;
}

#delete {
    background-color: red;
}
</style>