<template>
    <v-app>
      <!-- Barra de navegação lateral -->
      <v-navigation-drawer app>
        <!-- Conteúdo da barra de navegação -->
        <!-- Coloque aqui links para diferentes módulos, aulas, etc. -->
        <v-list>
          <v-list-item v-for="(module, index) in modules" :key="index" @click="selectModule(module)">
            <v-list-item-title>{{ module.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <!-- Conteúdo principal -->
      <v-main>
        <!-- Cabeçalho da página -->
        <v-app-bar app color="primary">
          <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
          <v-toolbar-title>Curso de Exemplo</v-toolbar-title>
        </v-app-bar>
  
        <!-- Conteúdo do curso -->
        <v-container class="py-4">
          <h2>{{ selectedModule.title }}</h2>
          <p>{{ selectedModule.description }}</p>
          <!-- Aqui você pode colocar o conteúdo específico do módulo selecionado -->
        </v-container>
      </v-main>
    </v-app>
  </template>
  
  <script lang="ts">
  export default {
    name: "ConteudoView",
    data() {
      return {
        drawer: true,
        modules: [
          {
            title: 'Módulo 1',
            description: 'Este é o primeiro módulo do curso. - Seus conteúdos serão liberados em breve'
          },
          {
            title: 'Módulo 2',
            description: 'Este é o segundo módulo do curso. - Seus conteúdos serão liberados em breve'
          },
          {
            title: 'Módulo 3',
            description: 'Este é o terceiro módulo do curso. - Seus conteúdos serão liberados em breve'
          },
          {
            title: 'Módulo 4',
            description: 'Este é o quarto e ultimo módulo do curso. - Seus conteúdos serão liberados em breve'
          },
          // Adicione mais módulos aqui conforme necessário
        ],
        selectedModule: {
          title: 'Seus conteúdos serão liberados em breve'
        }
      };
    },
    methods: {
      selectModule(module: { title: string; description: string; }) {
        this.selectedModule = module;
      }
    },

    mounted(){

    }
  };
  </script>