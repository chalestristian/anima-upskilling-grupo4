<template>  
<h1>Meus Cursos</h1>
<div class="card" v-for="matricula in matriculas" :key="matricula.id">

  <div class="card-header">
    <slot name="header"> {{matricula.curso.nome}}</slot>
  </div>
   
  <div class="card-body">
    <slot>CARGA HORÁRIA: {{matricula.curso.nome}}</slot>
  </div>

  <div class="card-btn">
    <slot name="header">
      <button @click="GerarBoleto(curso.valor, pessoa.nome, pessoa.cpf, pessoa.email, pessoa.celular)" style="background-color: rgb(101, 101, 101); color: WHITE;"  v-if="matricula.matriculaConfirmada == false"> Gerar Boleto </button>
      <button @click="" style="background-color: rgb(101, 101, 101); color: WHITE;"  v-if="matricula.matriculaConfirmada == true && matricula.status == '' "> Acessar Conteudo </button>
      <button @click="" style="background-color: rgb(101, 101, 101); color: WHITE;"  v-if="matricula.status == 'APROVADO'"> Gerar Certificado </button>

      </slot>
  </div>
</div>
  
<hr /> 

  <h1>Cursos Disponiveis</h1>
  <div class="card" v-for="(curso, index) in cursos" :key="index">
      <div class="card-header" >
      <slot name="header">CURSO: {{ curso.nome }}</slot>
    </div>
    <div class="card-body">
      <slot>CARGA HORÁRIA: {{ curso.ch }}h</slot>
    </div>
    <div class="card-footer">
      <slot name="footer">VALOR: R${{ curso.valor }}</slot>
    </div>
    <div class="card-footer">
      <button @click="RealizarMatricula(userId, curso.id, curso.valor)" style="background-color: rgb(101, 101, 101); color: WHITE;"> INSCREVER </button>
    </div>
  </div>


</template>
    
<script lang="ts">
import DataService from "../services/DataServices";
import {CursoModel} from '../models/CursoModel';
import { UsuarioModel } from "@/models/UsuarioModel";
import { MatriculaModel } from "@/models/MatriculaModel";
import type { IBoleto } from "@/interfaces/IPessoa copy";
import { BoletoModel } from "@/models/BoletoModel";
import { PessoaModel } from "@/models/PessoaModel";
let curso: CursoModel = new CursoModel();
let cursos: Array<CursoModel>;
let matricula: any;
var userid = localStorage.getItem("id")!;
let userId = parseInt(userid);
let usuario: UsuarioModel = new UsuarioModel();
let matriculas: MatriculaModel[] = new Array<MatriculaModel>();
let boleto: BoletoModel = new BoletoModel;
let pessoa: PessoaModel = new PessoaModel;
declare function atob(input: string): string;

export default {
    name: "DashboardView",
    data(){
      return{
        curso,
        cursos,
        matricula ,
        userId,
        usuario,
        matriculas,
        pessoa
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
       .then((response) => {this.matriculas = response.data, console.log(response.data)})
          .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},

    async GerarBoleto(valor: number, nome: string, cpf: string, email: string, celular: string){
      boleto.valor = valor;
      boleto.pagadorNome = nome;
      boleto.pagadorCpf = email;
      boleto.pagadorTelefone = celular;
      boleto.pagadorCpf = cpf;

         await DataService.Pdf(boleto).then((response)=>{
          console.log(response.data)

    const base64PDF = response.data
    const binaryPDF = atob(base64PDF);
    const pdfBytes = new Uint8Array(binaryPDF.length);
    for (let i = 0; i < binaryPDF.length; i++) {
        pdfBytes[i] = binaryPDF.charCodeAt(i);
    }
    const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });
    const pdfUrl = URL.createObjectURL(pdfBlob);
    window.open(pdfUrl);
        
      })},  

      async ListarPessoa(id: number){
    await DataService.ListarPessoa(this.userId)
    .then((response) => {this.pessoa = response.data})
    .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},

    
      
     

// pdf(){          
//         DataService.Pdf().then(function(response){
//           console.log(response.data)
//           const base64PDF = response.data;
//           const binaryPDF = atob(base64PDF);

//             // Criar um array de 8-bit unsigned integers (Uint8Array) a partir dos bytes do PDF
//             const pdfBytes = new Uint8Array(binaryPDF.length);
//             for (let i = 0; i < binaryPDF.length; i++) {
//                 pdfBytes[i] = binaryPDF.charCodeAt(i);
//             }

//             // Criar um objeto Blob a partir dos bytes do PDF
//             const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });

//             // Criar uma URL temporária para o PDF Blob
//             const pdfUrl = URL.createObjectURL(pdfBlob);

//             // Abrir o PDF em uma nova janela ou guia do navegador
//             window.open(pdfUrl);
//             }, function(response){
//     //Error
// });},

      async anyGetCursosMatriculados(id: number){
       await DataService.CursosMatriculados(id)
       .then((response) => {this.matriculas = response.data, console.log(response.data)})
          .catch(function (error) {
      if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},
    
          },

         
            
    mounted() {    
    
        this.GetUsuarioId(userId);
        this.GetCursosMatriculados(userId);  
        this.GetAllCursos(); 
       
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

.card-btn{
  margin-top: 10px;


}
    </style>