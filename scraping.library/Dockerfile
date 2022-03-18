FROM openjdk:15-jdk-slim as build
WORKDIR /library
RUN apt-get -y update
RUN echo "PWD: $PWD"
ARG JAR_FILE=target/*.jar
COPY ${JAR_FILE} app.jar
RUN java -Djarmode=layertools -jar app.jar extract

RUN addgroup slavegroup && adduser --ingroup slavegroup --disabled-password slave
USER slave

FROM openjdk:15-jdk-slim
WORKDIR /library
COPY --from=build library/dependencies/ ./
COPY --from=build library/spring-boot-loader/ ./
COPY --from=build library/snapshot-dependencies/ ./
COPY --from=build library/application/ ./
ENTRYPOINT ["java","-Dspring.profiles.active=docker", "org.springframework.boot.loader.JarLauncher"]
