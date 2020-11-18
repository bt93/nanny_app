<template>
  <div>
      <h1>New Parent/Guardian for {{ child.firstName }} {{ child.lastName }}</h1>
      <div class="addParent">
            <new-parent />
            <div class="exsitingParents">
                <h1>Exisisting Parent</h1>
                <ul>
                    <li v-for="parent in parents" :key="parent.parentId">
                        <h2>{{ parent.firstName }} {{ parent.lastName }}</h2>
                        <button>Add {{ parent.firstName }} as {{ child.firstName }}'s Parent/Guardian</button>
                    </li>
                </ul>
            </div>
      </div>
  </div>
</template>

<script>
import parentService from '../services/ParentService'
import NewParent from '../components/NewParent'

export default {
    components: {
        NewParent
    },
    data() {
        return {
            parents: [],
            error: false
        }
    },
    props: {
        child: Object
    },
    created() {
        parentService.getParents()
            .then(res => {
                if (res.status === 200) {
                    this.parents = res.data;
                }
            })
            .catch(err => {
                if (err) {
                    this.error = true;
                }
            });
    }
}
</script>

<style>
.addParent {
    display: flex;
    justify-content: space-around;
}
</style>