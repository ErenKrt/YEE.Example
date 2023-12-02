FROM node:18 as ikui
WORKDIR /app/ikui
EXPOSE 1883
COPY ../src/YEE.IK/YEE.IK.UI/package*.json ./
RUN npm install

FROM ikui as ikui-preview
COPY ../src/YEE.IK/YEE.IK.UI/ ./
RUN npm run build
CMD ["npm", "run", "preview"]
