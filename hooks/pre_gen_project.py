import os

namespace_search_text = "[[Namespace]]"
namespace_replace_text = "{{cookiecutter.Namespace}}"

project_search_text = "[[ProjectName]]"
project_replace_text = "{{cookiecutter.ProjectName}}"

project_lower_search_text = "[[ProjectNameLower]]"
project_lower_replace_text = "{{cookiecutter.ProjectName}}".lower().replace(".", "-")

namespace_project_lower_search_text = "[[NamespaceProjectNameLower]]"
namespace_project_lower_replace_text = "{{cookiecutter.Namespace}}.{{cookiecutter.ProjectName}}".lower().replace(".", "-")

directories = [
    "../../github_configs/workflows",
    "../../argocd",
    "../../charts/k8s-service/values"
]

for directory in directories:
    for filename in os.listdir(directory):
        if filename.endswith(".yaml") or filename.endswith(".yml"):
            file_path = os.path.join(directory, filename)
            with open(file_path, "r") as file:
                data = file.read()
                data = data.replace(namespace_search_text, namespace_replace_text)
                data = data.replace(project_search_text, project_replace_text)
                data = data.replace(project_lower_search_text, project_lower_replace_text)
                data = data.replace(namespace_project_lower_search_text, namespace_project_lower_replace_text)

            with open(file_path, "w") as file:
                file.write(data)

print("Text replaced")
