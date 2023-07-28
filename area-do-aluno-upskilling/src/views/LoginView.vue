<template>
  <v-container fluid>
    <v-row justify="center" align="center" class="login-row" >
      <v-col cols="12" sm="8" md="4" class="content">
        <v-card>
          <v-card-title class="headline">ENTRAR</v-card-title>
          <v-card-text class="inputbox" style="padding: 50px; background-color: #2576c815;">
            <input type="string" v-model="login.login" placeholder="LOGIN" style="border: 1px solid black; padding: 10px; margin: 10px 20px; font-size: 20px;font-weight: 500;"/>
            <input type="password" v-model="login.senha" placeholder="SENHA" style="border: 1px solid black; padding: 10px; margin: 10px 20px; font-size: 20px;font-weight: 500;"/>
            <v-btn color="rgba(155, 229, 206, 0.847)" style="padding: 15px; margin-bottom: 13px; padding-bottom: 30px;" type="submit" @click="Logar">ENTRAR</v-btn>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import DataService from "../services/DataServices";
import {AlunoModel} from '../models/AlunoModel';
import {PessoaModel} from '../models/PessoaModel';
import {UsuarioModel} from '../models/UsuarioModel';
import { LoginModel } from "@/models/LoginModel";

let aluno: AlunoModel = new AlunoModel();
let alunoStorage: AlunoModel = new AlunoModel();

let pessoa: PessoaModel = new PessoaModel();
let pessoaStorage: PessoaModel = new PessoaModel();

let usuario: UsuarioModel = new UsuarioModel();
let usuarioStorage: UsuarioModel = new UsuarioModel();

let login: LoginModel = new LoginModel();

export default {
name: "LoginView",
data(){
  return{
  aluno,
  pessoa,
  usuario,
  login,
  alunoStorage,
  pessoaStorage,
  usuarioStorage,
  }
},

methods: {
    async Logar(login: LoginModel){
    let data = await DataService.Logar(this.login)
    .then((response) => {
      if(response.status == 200){
        window.location.href = "/dashboard";
      }})  
    .catch(function (error) {
      if(error.response){
      window.alert("ERRO: [" + error.response.status + "] " + error.response.data)
    }   
    })
 }, 
},

mounted() {
  let usuario = localStorage.getItem("nome");
  if(usuario != null){
    window.location.href = "/dashboard";
  }
 
  
  }
}

</script>
<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Black+Ops+One&display=swap');


.login-row {
  height: 100vh;
}
.headline{
  font-family: 'Monoton', cursive;
  font-size: 1em;
  color: rgb(111, 111, 111);
  font-family: 'Black Ops One', cursive;
}
.content{
  background-color: rgba(10, 68, 112, 0.661);
}
.inputbox{
  padding: 500px 10px;
}
</style>