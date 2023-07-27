<template>
  <v-layout> 
    <v-main :elevation="0" class="d-flex" style="background-color: green;">
      <img src="../assets/img/ic.svg" alt="Logo">
      <a href="#">INICIO</a>
      <a href="#">SOBRE</a>
      <a href="#" class="me-auto">CONTATO</a>
      <a href="#">INSCRIÇÃO</a>
      <a href="#">ENTRAR</a>
    </v-main>
  </v-layout> 
</template>

<style scoped>
a{
  text-decoration: none;
  font-size: 1em;
  font-weight: 500;
  color: rgb(115, 115, 115);
  text-align: center;
  margin-left: 2px;  
  min-width: 80px;  
  max-width: 80px;
}
a:hover{
  text-decoration: underline;
  text-decoration-color: rgb(150, 150, 150);
}
img{
  margin-left: 2px;
  min-width: 50px;
  max-width: 50px;
}
</style>