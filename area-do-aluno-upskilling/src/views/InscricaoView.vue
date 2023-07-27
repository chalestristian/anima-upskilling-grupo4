<!-- 
  INSCRIÇÃO FOI MOVIDA PARA O MVC
  
  
  <template>
<div style="border: 1px solid red; padding: 10px;">
    <h1>Cria pessoa</h1>
    <input type="string" v-model="pessoa.nome" placeholder="nome" style="border: 1px solid black;"/>
    <input type="string" v-model="pessoa.cpf" placeholder="cpf" style="border: 1px solid black;"/>
    <input type="string" v-model="pessoa.celular" placeholder="celular" style="border: 1px solid black;"/>
    <input type="string" v-model="pessoa.email" placeholder="email" style="border: 1px solid black;"/>

    <input type="string" v-model="usuario.login" placeholder="login" style="border: 1px solid black;"/>
    <input type="string" v-model="usuario.senha" placeholder="login" style="border: 1px solid black;"/>

    <button @click="CriarPessoaAlunoUsuario" style="background-color: rgb(101, 101, 101); color: WHITE;"> ENVIAR </button>
</div>
</template>

<script lang="ts">
import DataService from "../services/DataServices";
import {AlunoModel} from '../models/AlunoModel';
import {PessoaModel} from '../models/PessoaModel';
import {UsuarioModel} from '../models/UsuarioModel';

let aluno: AlunoModel = new AlunoModel();
let alunoStorage: AlunoModel = new AlunoModel();

let pessoa: PessoaModel = new PessoaModel();
let pessoaStorage: PessoaModel = new PessoaModel();

let usuario: UsuarioModel = new UsuarioModel();
let usuarioStorage: UsuarioModel = new UsuarioModel()

export default {
name: "InscricaoView",
data(){
  return{
  aluno,
  pessoa,
  usuario,

  alunoStorage,
  pessoaStorage,
  usuarioStorage,
  }
},

methods: {
  async CriarPessoaAlunoUsuario(){
    await this.CriarPessoa(this.pessoa);
    await this.CriarAluno(this.aluno);
    await this.CriarUsuario(this.usuario);
  },

  async CriarPessoa(pessoa: PessoaModel){
    await DataService.CriarPessoa(this.pessoa)
    .then((response) => {this.pessoaStorage = response})
    .catch(e => {console.log(e);})
  },

  async CriarAluno(aluno: AlunoModel){
    this.aluno.pessoaId = this.pessoaStorage.id;
    this.aluno.matricula = this.MatriculaRandomGenerator()!.toString();
    
    await DataService.CriarAluno(this.aluno)
    .then((response) => {this.alunoStorage = response})
    .catch(e => {console.log(e);})
  },

  async CriarUsuario(usuario: UsuarioModel){
    this.usuario.pessoaId = this.pessoaStorage.id;
    this.usuario.alunoId = this.alunoStorage.id;

    await DataService.CriarUsuario(this.usuario)
    .then((response) => {this.usuarioStorage = response})
    .catch(e => {console.log(e);})
  },

  MatriculaRandomGenerator(){
    const array = new Uint32Array(10);
    self.crypto.getRandomValues(array);
    for (const num of array) {
      return num;
    }
  },
  },

  mounted() {
  }
}
</script>

<style>

</style> -->