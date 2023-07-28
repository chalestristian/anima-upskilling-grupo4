<template>  
<button @click="logout()">LOGOUT</button>
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
      <button @click="GerarBoleto(matricula.valorMatricula, pessoa.nome, pessoa.cpf, pessoa.email, pessoa.celular)" v-if="matricula.matriculaConfirmada == false" class="btn gerarboleto"> GERAR BOLETO </button>
      <a href="/conteudo"><button v-if="matricula.matriculaConfirmada == true && matricula.status != 'APROVADO'" class="btn acessarconteudo"> ACESSAR CONTEÚDO </button></a>
      <button @click="SolicitarCertificado(matricula.aluno.id)" v-if="matricula.status == 'APROVADO'" class="btn gerarcertificado"> SOLICITAR CERTIFICADO </button>
     </slot>
  </div>
</div>
  
<hr /> 

<h1>Cursos Disponiveis</h1>
<div>

</div>
  <div class="card" v-for="curso in cursos" :key="curso.id">
    <div class="card-header">
      <slot name="header">CURSO: {{ curso.nome }}</slot>
    </div>
    
    <div class="card-body">
      <slot>CARGA HORÁRIA: {{ curso.ch }}h</slot>
    </div>
    
    <div class="card-footer">
      <slot name="footer">VALOR: R${{ curso.valor }}</slot>
    </div>
    
    <div class="card-footer">
      <button @click="RealizarMatricula(userId, curso.id, curso.valor)" class="btn inscrever"> INSCREVER </button>
    </div>
</div>
</template>
    
<script lang="ts">
import DataService from "../services/DataServices";
import {CursoModel} from '../models/CursoModel';
import { UsuarioModel } from "@/models/UsuarioModel";
import { MatriculaModel } from "@/models/MatriculaModel";
import { BoletoModel } from "@/models/BoletoModel";
import { PessoaModel } from "@/models/PessoaModel";
let curso: CursoModel = new CursoModel();
let cursos: Array<CursoModel>;
let matricula: MatriculaModel = new MatriculaModel();
var userid = localStorage.getItem("id")!;
let userId = parseInt(userid);
let usuario: UsuarioModel = new UsuarioModel();
let matriculas: MatriculaModel[] = new Array<MatriculaModel>();
let boleto: BoletoModel = new BoletoModel;
let pessoa: PessoaModel = new PessoaModel();
declare function atob(input: string): string;
let boletobase64: string;
let certificado: string;

export default {
    name: "DashboardView",
    data(){
      return{
        curso,
        cursos,
        matricula,
        boletobase64,
        certificado,
        matriculas,
        usuario,
        userId,
        pessoa
      }
    },
    
    methods: {
      
      async logout(){
        localStorage.clear();
        window.location.href = "http://localhost:5173/";    
        },

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
        boleto.pagadorEmail = email;
        boleto.pagadorCpf = cpf;
        boleto.pagadorTelefone = celular;
          
        await DataService.BoletoPdf(boleto)
        .then((response) => {this.boletobase64 = response.data })

        const binaryPDF = atob( this.boletobase64);
        const pdfBytes = new Uint8Array(binaryPDF.length);
        for (let i = 0; i < binaryPDF.length; i++) {pdfBytes[i] = binaryPDF.charCodeAt(i);}

        const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });
        const pdfUrl = URL.createObjectURL(pdfBlob);
        window.open(pdfUrl);},  

      async ListarPessoa(id: number){
        await DataService.ListarPessoa(this.userId)
        .then((response) => {this.pessoa = response.data})
        .catch(function (error) {
        if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},
   

      async anyGetCursosMatriculados(id: number){
        await DataService.CursosMatriculados(id)
        .then((response) => {this.matriculas = response.data})
        .catch(function (error) {
        if(error.response){window.alert("ERRO: [" + error.response.status + "] " + error.response.data)}})},

     
      async SolicitarCertificado(id: number){
        await DataService.SolicitarCetificado(id);

        let  certificado: string;
      async function miFuncion() {
        await DataService.GetCetificado(id)
        .then((response) => {certificado = response.data, console.log("certificado", response.data)})

        const binaryPDF = atob(certificado);
        const pdfBytes = new Uint8Array(binaryPDF.length);
        
        for (let i = 0; i < binaryPDF.length; i++) {
        pdfBytes[i] = binaryPDF.charCodeAt(i);
         }

      const pdfBlob = new Blob([pdfBytes], { type: 'application/pdf' });
      const pdfUrl = URL.createObjectURL(pdfBlob);
      window.open(pdfUrl);  
        }
        setTimeout(miFuncion, 1000);



       
    },
  },

    mounted() {   
      this.ListarPessoa(userId)
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

.gerarboleto{
  background-color: rgba(255, 239, 19, 0.909);
}
.acessarconteudo{
  background-color: rgba(24, 128, 219, 0.909);

}
.gerarcertificado{  
  background-color: rgba(0, 26, 255, 0.362);
}
.inscrever{
  background-color: rgb(7, 248, 163);

}
.btn{
  font-weight: 500;
  color: rgb(0, 0, 0);
  font-size: 15px;
  padding: 5px;
  border-radius: 10px;
}
</style>