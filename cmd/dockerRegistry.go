package cmd

import (
	"os/exec"

	log "github.com/sirupsen/logrus"
)

var DOCKER_REGISTRY_NAME string = "docker-image-reg"

func installDockerRegistry() {
	log.Info("Starting " + DOCKER_REGISTRY_NAME)
	cmd := exec.Command(
		"docker",
		"run",
		"-d",
		"-p5000:5000",
		"--pull=missing",
		"--name="+DOCKER_REGISTRY_NAME,
		"--restart=always",
		"registry:2",
	)

	out, err := cmd.CombinedOutput()
	if err != nil {
		log.Infof("%s", out)
		log.Fatalf("failed to start "+DOCKER_REGISTRY_NAME+": %v", err)
	}

	// connect to kind network
	cmd = exec.Command(
		"docker",
		"network",
		"connect",
		"kind",
		DOCKER_REGISTRY_NAME,
	)

	out, err = cmd.CombinedOutput()
	if err != nil {
		log.Infof("%s", out)
		log.Fatalf("failed to add "+DOCKER_REGISTRY_NAME+"to the kind network: %v", err)
	}
	log.Info("attach " + DOCKER_REGISTRY_NAME + "to kind network")

}

func removeDockerRegistryDockerContainer() {
	log.Info("Removing container " + DOCKER_REGISTRY_NAME + " from docker")

	//stop the git-server container
	dockerStopCmd := exec.Command(
		"docker",
		"stop",
		DOCKER_REGISTRY_NAME,
	)
	var dockerStopErr error
	dockerStopErr = dockerStopCmd.Run()
	if dockerStopErr != nil {

		log.Infof("Docker container \""+DOCKER_REGISTRY_NAME+"\" failed to stop:  %v", dockerStopErr)
	}

	//remove the git-server container
	dockerRemoveCmd := exec.Command(
		"docker",
		"rm",
		DOCKER_REGISTRY_NAME,
	)
	var dockerRemoveErr error
	dockerRemoveErr = dockerRemoveCmd.Run()
	if dockerRemoveErr != nil {

		log.Infof("Docker container \""+DOCKER_REGISTRY_NAME+"\" failed to be removed:  %v", dockerRemoveErr)
	}

	log.Info(DOCKER_REGISTRY_NAME + " removed from docker")

}