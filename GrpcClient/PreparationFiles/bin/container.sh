docker run -itd --rm --privileged --name $2 -v $PWD/ExecuteFiles/$2/Data:/opt/Data -v $PWD/ExecuteFiles/$2/bin:/opt/bin/ $1
