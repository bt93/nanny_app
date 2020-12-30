<template>
  <v-container>
      <h2>New Parent/Guardian for {{ child.firstName }} {{ child.lastName }}</h2>
      <v-container class="addParent">
        <div class="exsitingParents">
            <h3>Exisisting Parent</h3>
            <v-virtual-scroll
                v-if="parents.length > 0"
                height="200"
                width="1000"
                item-height="80"
                :items="checkParent"
            >
                <template v-slot:default="{ item }">
                    <v-list-item>
                        <v-btn>Add {{ item.firstName }} to be {{ child.firstName }}s Parent/Gaurdian</v-btn>
                    </v-list-item>
                </template>
            </v-virtual-scroll>
            <h3 v-else>You have no parents in our database.</h3>
        </div>
      </v-container>
  </v-container>
</template>

<script>
import parentService from '@/services/ParentService'
//import NewParent from '@/components/NewParent'

export default {
    components: {
        //NewParent
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
    },
    computed: {
        checkParent() {
            return this.parents.map(e => {
                return this.child.parents.filter(i => i.parentId == e.parentId);
            })
        }
    }
}
</script>

<style>
.addParent {
    display: flex;
    justify-content: space-around;
}
</style>