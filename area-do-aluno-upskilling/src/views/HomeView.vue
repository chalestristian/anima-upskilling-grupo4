<template>  

<v-container>

  <v-row justify="end" >
    <v-col cols="12">
      <v-sheet align="right">
        <v-btn elevation="20" size="large" class="btn" href="https://localhost:5700/Home">INSCREVA-SE</v-btn>
        <v-btn elevation="20" size="large" class="btn" href="/login">LOGIN</v-btn>
      </v-sheet>
    </v-col>
  </v-row>

  <v-row justify="center" style="padding: 20vh 30px;">
    <v-col cols="6">
      <v-sheet xs="12" style="border-top: 1px">
          <h1>Defina seu futuro!</h1> 
      </v-sheet>
    </v-col>

    <v-col cols="6">
      <v-sheet xs="12">
        <h2>Ou seu futuro definirá você?</h2>
      </v-sheet>
    </v-col>
  </v-row>

  <v-row justify="start">
    <v-col cols="6" xs="12">
      <v-sheet xs="12" align="center">
        <img src="../assets/img/home-img/img01.svg" alt="" class="img-content" width="500" height="600">
      </v-sheet>
    </v-col>

    <v-col cols="6" xs="12">
      <v-sheet class="text" align="center">
        <div class="contentum">
          <p class="titulo">Cursos Completos</p>
            <span class="bolha">Frontend</span>
            <span class="bolha">Backend</span>
            <span class="bolha">Web Design</span>
            <span class="bolha">UX/UI Design</span>
            <span class="bolha">HTML</span>
            <span class="bolha">CSS</span>
            <span class="bolha">JavaScript</span>
            <span class="bolha">TypeScript</span>
            <span class="bolha">React, Vue, Angular</span>
          <p class="titulo">Aprenda do zero Web Design e comece a sua carreira de UX / UI Designer e Desenvolvedor(a).</p>
        </div>
      </v-sheet>
    </v-col>
  </v-row>


  <v-row justify="start">
    <v-col cols="6" xs="12">
      <v-sheet class="text" align="center">
        <div class="content">
          <p>
            Os cursos da Upskilling são voltados para o desenvolvimento divididos
            em várias categorias entre Front-End, Back-End e UX/UI Design.
            Uma lista com vários cursos disponíveis.
          </p>
        </div>
      </v-sheet>
    </v-col>

    <v-col cols="6" xs="12">
      <v-sheet xs="12" align="center">
        <img src="../assets/img/home-img/img04.svg" alt="" class="img-content" width="500" height="600">
      </v-sheet>
    </v-col>
  </v-row>

  <v-row justify="center">
    <v-col cols="12">
      <v-sheet align="center">
        <v-btn elevation="20" size="large" class="btn">INSCREVA-SE</v-btn>
        <v-btn elevation="20" size="large" class="btn">LOGIN</v-btn>
      </v-sheet>
    </v-col>
  </v-row>      

</v-container>

</template>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Comfortaa:wght@700&display=swap');
.content{
  padding: 50px;
  margin-top: 150px;
}
.contentum{
  padding: 30px;  
}
.titulo{
  font-size: 1em; 
  color: black;
}
.bolha{
  font-size: 0.8em;
  border-radius: 50px;
  float: left;
  margin: 35px;
  padding: 10px;
  border: 3px dashed rgb(102, 102, 102);
}
p{
  clear: both;
}
.text{
  font-family: 'Comfortaa', cursive;
  font-size: 2em;
  height: 609px;
  color: rgb(98, 95, 95);
}
.img-content{
  width: 100%;
}
.btn{
  font-size: 20px;
  font-family: 'Comfortaa', cursive;
  color: white;
  margin: 15px 30px;
  background-color: rgba(38, 131, 101, 0.847);
}
.btn:hover{
  color: rgb(27, 27, 27);
  background-color: rgba(155, 229, 206, 0.847);
}
h1, h2{
  padding: 10px 50px;
  font-size: 4em;
  color: rgb(69, 69, 69);
  font-weight: 900;
  font-family: 'Comfortaa', cursive;
}
h2{
  background-color: black;
  color: rgb(218, 218, 218);
}
a{
  text-decoration: none;
}
a:hover{
  text-decoration: none;
}
@media only screen and (max-width: 2559px) {
h1,h2{
  font-size: 3em;
}
}
@media only screen and (max-width: 1919px) {
.bolha{
  font-size: 0.6em;
  margin: 20px;
}
.titulo{
  font-size: 0.8em; 
}
.content{
  margin-top: 90px;
}
h1,h2{
  padding: 10px;
  font-size: 2.3em;
}
}

@media only screen and (max-width: 1279px) {
h1,h2{
  padding: 5px;
  font-size: 1.6em;
}
.bolha{
 font-size: 0.5em;
 margin: 15px;
}
.content{
 margin-top: 20px;
}
}
@media only screen and (max-width: 900px) {
.bolha{
 font-size: 0.4em;
 margin: 15px 10px;
}
.content{
 margin-top: 0px;
}
h1,h2{
 padding: 2px;
 font-size: 1.2em;
}
}
@media only screen and (max-width: 850px) {
.titulo{
 font-size: 0.7em;
}
.content{
 font-size: 0.8em;
}
}
@media only screen and (max-width: 730px) {
.titulo{
 font-size: 0.5em;
}
.content{
 font-size: 0.7em;
} 
}
</style>