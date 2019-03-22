workflow "Main Workflow" {
  on = "push"
  resolves = ["npm"]
}

action "npm" {
  uses = "docker://node"
  runs = "ls"
}

