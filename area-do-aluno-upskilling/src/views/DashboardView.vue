<template>  
 <h1>Meus Cursos</h1>
<div class="card" v-for="item in cursosmatriculados" :key="cursosmatriculados">
      <div class="card-header">
      <slot name="header"> {{cursosmatriculados}}</slot>
    </div>
    <div class="card-body">
      <slot>CARGA HORÁRIA: {{ cursosmatriculados }}h</slot>
    </div>
  </div>  
  <hr /> 
  <h1>Cursos Disponiveis</h1>
  <div class="card" v-for="(item, index) in cursos" :key="index">
    <div class="card-header">
      <slot name="header">CURSO: {{ item.nome }}</slot>
    </div>
    <div class="card-body">
      <slot>CARGA HORÁRIA: {{ item.ch }}h</slot>
    </div>
    <div class="card-footer">
      <slot name="footer">VALOR: R${{ item.valor }}</slot>
    </div>
    <div class="card-footer">
      <button @click="RealizarMatricula(userId, item.id, item.valor)" style="background-color: rgb(101, 101, 101); color: WHITE;"> Comprar </button>
    </div>
  </div>

</template>
    

<script lang="ts">
import DataService from "../services/DataServices";
import {CursoModel} from '../models/CursoModel';
import { UsuarioModel } from "@/models/UsuarioModel";
import { CursosMatriculaModel } from "@/models/CursosMatriculaModel";
let curso: CursoModel = new CursoModel();
let cursos: Array<CursoModel>;
let matricula: any;
var userid = localStorage.getItem("id")!;
let userId = parseInt(userid);

let usuario: UsuarioModel = new UsuarioModel();
let cursosmatriculados: CursosMatriculaModel[] = new Array<CursosMatriculaModel>();

export default {
    name: "DashboardView",
    data(){
      return{
        curso,
        cursos,
        matricula ,
        userId,
        usuario,
        cursosmatriculados
      }
    },
    
    methods: {
    
    async GetAllCursos(){
    await DataService.ListarCursos()
    .then((response) => {this.cursos = response})
    .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})}, 
    
    async RealizarMatricula(idAluno: number, idCurso: number, valor: number){
    await DataService.Matricular(idAluno, idCurso, valor)
    .then((response) => {this.matricula = response.data})
    .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},

    
    async GetUsuarioId(userId: number){
    await DataService.ListarUsuarioPorId(userId)
    .then((response) => {this.usuario = response.data})
    .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})}, 
    
      async GetCursosMatriculados(id: number){
       await DataService.CursosMatriculados(id)
       .then((response) => {console.log(response)})
          .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})}, 
      
    
    
    mounted() {       
        this.GetUsuarioId(userId);
        this.GetCursosMatriculados(userId);  
        this.GetAllCursos()

      }
    }
  }     
    
    </script>
    
    <style>
.card {
  margin: 50px 700px;
  border: 1px solid #ccc;
  border-radius: 4px;
  padding: 1rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.card-header {
  font-weight: bold;
  border-bottom: 1px solid #ccc;
  padding-bottom: 0.5rem;
  margin-bottom: 0.5rem;
}

.card-footer {
  border-top: 1px solid #ccc;
  padding-top: 0.5rem;
  margin-top: 0.5rem;
}
    </style>