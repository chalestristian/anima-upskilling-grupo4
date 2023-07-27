<template>
  <v-layout>
    <v-main :elevation="0" style="background-color: blue; ">
      <section>
        <h1>Defina seu futuro!</h1>
        <h2>Ou seu futuro definirá você.</h2>
      </section>

      <section>
        <h3>
          Uma ampla plataforma de cursos. <br />
          Escolha entre mais de 500.000 cursos online com novos cursos lançados todos os meses
        </h3>
      </section>

      <section>
        <v-container>
          <v-row align="center" justify="center">
            <v-col cols="auto">
              <v-btn size="x-large">INSCRIÇÃO</v-btn>
            </v-col>
            <v-col cols="auto">
              <v-btn size="x-large">LOGIN</v-btn>
            </v-col>
          </v-row>
        </v-container>
      </section>

    </v-main>
  </v-layout> 
</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Bungee+Shade&display=swap');
@import url('https://fonts.googleapis.com/css2?family=Bungee&display=swap');
h1{
  text-align: center;
  font-family: 'Bungee Shade', cursive;
  color: rgb(38, 38, 38);
  font-size: 80px;
  line-height: 1;
}
h2{
  margin-top: 50px;
  text-align: center;
  font-family: 'Bungee', cursive;
  color: rgb(73, 73, 73);
  font-size: 30px;
  line-height: 1;
}
h3{
  margin-top: 50px;
  text-align: center;
  color: rgb(73, 73, 73);
  font-size: 20px;
  line-height: 1;
}
section{
  margin-top: 10px;
  padding-bottom: 10px;
}
.v-btn{
  font-size: 20px;
  background-color: rgb(211, 211, 211);
}
</style>