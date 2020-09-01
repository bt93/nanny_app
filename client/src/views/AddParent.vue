<template>
  <div class="addParent">
      <new-parent />
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

</style>