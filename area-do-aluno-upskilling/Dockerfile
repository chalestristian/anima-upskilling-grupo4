FROM node:lts

WORKDIR /app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

RUN npm install esbuild@0.18.15 --save-dev

RUN npm run build

RUN npm install -g serve

EXPOSE 5800

CMD ["serve", "-s", "dist", "-l", "5800"]
