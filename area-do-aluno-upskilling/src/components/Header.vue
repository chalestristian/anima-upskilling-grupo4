<template>
<v-layout> 
    <v-app-bar :elevation="0" style="background-color: red; height: 54px;">
      <img src="../assets/img/ic.svg" alt="Logo">
      <a href="#">INICIO</a>
      <a href="#">SOBRE</a>
      <a href="#" class="me-auto">CONTATO</a>
      <a href="#">INSCRIÇÃO</a>
      <a href="#">ENTRAR</a>
    </v-app-bar>
  </v-layout> 

<!-- 
  <v-container fluid class="bg-surface-variant mb-6">
    <div>
    <v-container class="bg-surface-variant mb-6">
      <v-row align="start" no-gutters style="height: 150px;"
      >
        <v-col
          v-for="n in 3"
          :key="n"
        >
          <v-sheet class="pa-2 ma-2">
            .align-start
          </v-sheet>
        </v-col>
      </v-row>
    </v-container> 

   
</div></v-container> -->
    





  
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