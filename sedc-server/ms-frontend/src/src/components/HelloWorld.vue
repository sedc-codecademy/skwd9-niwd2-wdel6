<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>
      For a guide and recipes on how to configure / customize this project,<br>
      check out the
      <a href="https://cli.vuejs.org" target="_blank" rel="noopener">vue-cli documentation</a>.
    </p>
    <div>
      <div v-for="author in authors" v-bind:key="author.id">
        <h2>{{ author.ID }}</h2>
        <p>{{ author.Name }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      authors: [],
    };
  },
  methods: {
    async getData() {
      try {
        const response = await fetch("http://localhost:8083/api/authors");
        const data = await response.json();
        // JSON responses are automatically parsed.
        this.authors = data.Message;
      } catch (error) {
        console.log(error);
      }
    },
  },
  created() {
    this.getData();
  },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
