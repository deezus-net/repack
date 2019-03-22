workflow "Main Workflow" {
  on = "push"
  resolves = ["npm"]
}

action "npm" {
  uses = "docker://node"
  runs = "npm prefix ./repack install ./repack"
}

