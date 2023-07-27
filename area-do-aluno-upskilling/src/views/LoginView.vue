<template>
  <v-container fluid>
    <v-row justify="center" align="center" class="login-row">
      <v-col cols="12" sm="8" md="4">
        <v-card>
          <v-card-title class="headline">Login</v-card-title>
          <v-card-text>
            <input type="string" v-model="login.login" placeholder="login" style="border: 1px solid black;"/>
            <input type="password" v-model="login.senha" placeholder="senha" style="border: 1px solid black;"/>
            <v-btn color="primary" type="submit" @click="Logar">Login</v-btn>
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

<style>
.login-row {
  height: 100vh;
}

</style>