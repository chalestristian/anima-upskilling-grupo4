import axios, { Axios } from 'axios';
import {API_PATH}  from '../enviroment';
import { TOKEN_PATH } from '@/tokenacess';
import type { IPessoa } from '@/interfaces/IPessoa';
import type { IUsuario} from '@/interfaces/IUsuario';
import { GRANT_TYPE } from '@/tokenacess';
import { CLIENT_ID } from '@/tokenacess';
import { CLIENT_SECRET } from '@/tokenacess';
import { SCOPE } from '@/tokenacess';
import type { LoginModel } from '@/models/LoginModel';
import type { ILogin } from '@/interfaces/ILogin';
import type { ICurso } from '@/interfaces/ICurso';
import type { IMatricula } from '@/interfaces/IMatricula';
import type { BoletoModel } from '@/models/BoletoModel';

let Pessoa: IPessoa;
let Usuario: IUsuario;
let Login: ILogin;
let Cursos: ICurso;
let Matriculas: IMatricula;

 class DataService {

  async autenticar(){
    const params = new URLSearchParams();
    params.append('GRANT_TYPE', `${GRANT_TYPE}`); params.append('CLIENT_ID', `${CLIENT_ID}`);
    params.append('CLIENT_SECRET', `${CLIENT_SECRET}`); params.append('SCOPE', `${SCOPE}`);
    await axios.post(`${TOKEN_PATH}`, params).then((response) => {
    axios.defaults.headers.common['Authorization'] =  `Bearer ${response.data.access_token}`;
    axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*'; });
  }

  async Logar(login: LoginModel){
    await this.autenticar();
    let data = (await axios.post<typeof Login>(`${API_PATH}/Usuarios/Login`, login));
    this.salvaLocalStorage(data.data);
    return data;
  }

  async salvaLocalStorage(data: ILogin){
    localStorage.setItem("id", `${data.id}`);
    localStorage.setItem("idPessoa", `${data.pessoa.id}`);
    localStorage.setItem("nome", `${data.pessoa.nome}`);
    localStorage.setItem("email", `${data.pessoa.email}`);
    localStorage.setItem("telefone", `${data.pessoa.celular}`);
    localStorage.setItem("idAluno", `${data.aluno.id}`);
    localStorage.setItem("matricula", `${data.aluno.matricula}`);
  }

  async ListarCursos() {
    await this.autenticar();
    var data = await axios.get<Array<typeof Cursos>>(`${API_PATH}/Cursos`);
    return data.data;
  }

  async Matricular(idAluno: number, idCurso: number, valor: number) {
    await this.autenticar();
    var data = await axios.post(`${API_PATH}/Matriculas`,{
    alunoId: `${idAluno}`, cursoId: `${idCurso}`, valorMatricula:`${valor}`});
    return data;
  }
    
  async ListarUsuarioPorId(id: number) {
    await this.autenticar();
    var data = await axios.get<typeof Usuario>(`${API_PATH}/Usuarios/${id}`);
    return data;
  }

  async CursosMatriculados(id: number) {
    await this.autenticar();
    var data = (await axios.get<Array<typeof Matriculas>>(`${API_PATH}/Matriculas/ByAlunoCurso?id=${id}`));
    return data;
  }

  async BoletoPdf(boleto: BoletoModel){
    console.log("aqui???", boleto)
    let data = (await axios.post("http://localhost:5400/api/Boleto", boleto))
    return data;
  }       
  
  async ListarPessoa(id: number) {
    await this.autenticar();
    let data = (await axios.get<typeof Pessoa>(`${API_PATH}/Pessoas/${id}`));
    return data;
  }
      
  async GerarCetificado(id: number){
    await this.autenticar();
    let data = (await axios.post(`${API_PATH}/Certificado/GerarCertificado/${id}`))
    return data;
  }
               
  async SolicitarCetificado(id: number){
    await this.autenticar();
    let data = (await axios.post(`${API_PATH}/Certificado/SolicitarCertificado/${id}`))
    return data;
  }

  async GetCetificado(id: number){
    await this.autenticar();
    let data = (await axios.post(`${API_PATH}/Certificado/GetUrlCertificado/${id}`))
    return data;
  } 
}

export default new DataService();