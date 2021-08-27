<template>
    <mdb-alert color="danger" v-if="Show" @closeAlert="Show=false" dismiss>
        <strong style="font-weight: bold">Oops!</strong> {{Msg}}
    </mdb-alert>
</template>

<script>

export default {
    name: 'Category',
    props:{
        Msg: String,
        Show: Boolean
    },
}
</script>
